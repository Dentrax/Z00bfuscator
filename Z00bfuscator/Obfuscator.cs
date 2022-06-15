#region License
// ====================================================
// Z00bfuscator Copyright(C) 2013-2019 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;
using System.IO;
using System.Xml;
using System.Threading;
using System.Collections.Generic;

using Mono.Cecil;
using System.Text;
using System.Linq;

namespace Z00bfuscator
{
    public delegate void DelOutputEvent(string message);
    public delegate void DelNameObfuscated(ObfuscationItem item, string initialName, string obfuscatedName);
    public delegate void DelProgress(string phaseName, int percents);

    public sealed partial class Obfuscator : ObfuscatorBase, IObfuscator {

        #region Variables

        private Dictionary<string, string> m_mapResources = new Dictionary<string, string>();

        private List<AssemblyDefinition> m_assemblyDefinitions = new List<AssemblyDefinition>();

        private Dictionary<string, string> m_mapObfuscatedNamespaces = new Dictionary<string, string>();

        private Dictionary<string, bool> m_assemblies = new Dictionary<string, bool>();

        private List<string> m_excludedTypes = new List<string>();

        private ObfuscationInfo m_obfuscationInfo;

        private ObfuscationProgress m_obfuscationProgress;

        private XmlDocument m_xmlDocument;
        private XmlElement m_xmlElement;

        public event DelOutputEvent OnOutputEvent;
        public event DelNameObfuscated OnNameObfuscated;
        public event DelProgress OnProgress;

        #endregion

        #region Constructor

        public Obfuscator(ObfuscationInfo obfuscationInfo) {
            this.m_obfuscationInfo = obfuscationInfo;
            this.m_obfuscationProgress = new ObfuscationProgress(); 
        }

        #endregion

        #region AddAssembly

        public void AddAssembly(string path, bool obfuscate) {
            m_assemblies.Add(path, obfuscate);
        }

        #endregion

        #region ExcludeType

        public void ExcludeType(string typeName) {
            m_excludedTypes.Add(typeName);
        }

        #endregion

        #region Events
        protected override void UpdateProgress(string message, int percent) {
            OnProgress?.Invoke(message, percent);
        }

        protected override void LogProgress(string message) {
            OnOutputEvent?.Invoke(message);
        }

        #endregion

        #region StartObfuscation

        public void StartObfuscation() {
            Thread thread = new Thread(new ThreadStart(AsyncStartObfuscation));
            thread.Start();
        }

        protected override void AsyncStartObfuscation() {
            List<string> assembliesPaths = new List<string>();
            List<bool> assembliesToObfuscate = new List<bool>();

            LogProgress("[0]: Starting...");

            this.m_xmlDocument = new XmlDocument();
            this.m_xmlElement = this.m_xmlDocument.CreateElement("mappings");
            this.m_xmlDocument.AppendChild(m_xmlElement);

            UpdateProgress("[1]: Loading assemblies...", 10);
            foreach (string assemblyPath in m_assemblies.Keys) {
                try {
                    AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(assemblyPath);
                    foreach (ModuleDefinition module in assembly.Modules)
                        LogProgress($"[OK]: Module loaded: {module.Name}");

                    this.m_assemblyDefinitions.Add(assembly);
                    assembliesPaths.Add(Path.GetFileName(assemblyPath));
                    assembliesToObfuscate.Add(m_assemblies[assemblyPath]);
                } catch (Exception ex) {
                    LogProgress($"[ERR]: Module load failed: {ex.Message}");
                    continue;
                }
            }

            UpdateProgress("[2]: Starting obfuscate...", 20);

            int progressCurrent = 20;
            int assemblyIndex = -1;

            if (this.m_assemblyDefinitions.Any())
            {
                int progressIncrement = 60 / this.m_assemblyDefinitions.Count;

                foreach (AssemblyDefinition assembly in m_assemblyDefinitions)
                {
                    assemblyIndex++;

                    if (!assembliesToObfuscate[assemblyIndex])
                        continue;

                    LogProgress("Obfuscating assembly: " + assembly.Name.Name);

                    LogProgress("Obfuscating Types");
                    foreach (TypeDefinition type in assembly.MainModule.Types)
                        DoObfuscateType(type);

                    if (m_obfuscationInfo.ObfuscateNamespaces)
                        LogProgress("Obfuscating Namespaces");
                    foreach (TypeDefinition type in assembly.MainModule.Types)
                        DoObfuscateNamespace(type);

                    if (m_obfuscationInfo.ObfuscateResources)
                        LogProgress("Obfuscating Resources");
                    foreach (Resource resource in assembly.MainModule.Resources)
                        DoObfuscateResource(resource);

                    progressCurrent += progressIncrement;
                }
            }

            UpdateProgress("[3]: Saving assembly...", 80);

            assemblyIndex = -1;
            foreach (AssemblyDefinition assembly in m_assemblyDefinitions) {
                assemblyIndex++;

                if (Directory.Exists(this.m_obfuscationInfo.OutputDirectory) == false)
                    Directory.CreateDirectory(this.m_obfuscationInfo.OutputDirectory);

                string outputFileName = Path.Combine(this.m_obfuscationInfo.OutputDirectory, "Obfuscated_" + assembliesPaths[assemblyIndex]);

                if (File.Exists(outputFileName))
                    File.Delete(outputFileName);

                assembly.Write(outputFileName);
            }

            this.m_xmlDocument.Save(Path.Combine(m_obfuscationInfo.OutputDirectory, "Mapping.xml"));

            UpdateProgress("[4]: Testing assembly...", 90);

            foreach (string assemblyPath in this.m_assemblies.Keys) {
                if (!File.Exists(assemblyPath)) {
                    LogProgress($"[FAIL]: File not exists: {assemblyPath}");
                    continue;
                }

                try {
                    AssemblyDefinition assembly = AssemblyDefinition.ReadAssembly(assemblyPath);
                    foreach (ModuleDefinition module in assembly.Modules)
                        LogProgress($"[OK]: {module.Name}");

                } catch (Exception ex) {
                    LogProgress($"[FAIL]: {assemblyPath} - Exception: {ex.Message}");
                }

            }

            UpdateProgress("[5]: Complete.", 100);
        }

        #endregion

        #region DoObfuscateItem

        internal string DoObfuscateItem(ObfuscationItem item, string initialName) {
            string obfuscated = string.Empty;

            switch (item) {
                case ObfuscationItem.Method:
                    if (!this.m_obfuscationInfo.ObfuscateMethods)
                        return initialName;
                    this.m_obfuscationProgress.CurrentObfuscatedMethodID++;
                    obfuscated = this.GetObfuscatedFormat(item, initialName, this.m_obfuscationProgress.CurrentObfuscatedMethodID);
                    break;

                case ObfuscationItem.Type:
                    if (!this.m_obfuscationInfo.ObfuscateTypes)
                        return initialName;
                    this.m_obfuscationProgress.CurrentObfuscatedTypeID++;
                    obfuscated = this.GetObfuscatedFormat(item, initialName, this.m_obfuscationProgress.CurrentObfuscatedTypeID);
                    break;

                case ObfuscationItem.Namespace:
                    this.m_obfuscationProgress.CurrentObfuscatedNamespaceID++;
                    obfuscated = this.GetObfuscatedFormat(item, initialName, this.m_obfuscationProgress.CurrentObfuscatedNamespaceID);
                    break;

                case ObfuscationItem.Property:
                    if (!this.m_obfuscationInfo.ObfuscateProperties)
                        return initialName;
                    this.m_obfuscationProgress.CurrentObfuscatedPropertyID++;
                    obfuscated = this.GetObfuscatedFormat(item, initialName, this.m_obfuscationProgress.CurrentObfuscatedPropertyID);
                    break;

                case ObfuscationItem.Field:
                    if (!this.m_obfuscationInfo.ObfuscateFields)
                        return initialName;
                    this.m_obfuscationProgress.CurrentObfuscatedFieldID++;
                    obfuscated = this.GetObfuscatedFormat(item, initialName, this.m_obfuscationProgress.CurrentObfuscatedFieldID);
                    break;
            }

            OnNameObfuscated?.Invoke(item, initialName, obfuscated);

            this.AddToXMLMap(item, initialName, obfuscated);

            return obfuscated;
        }

        string GetObfuscatedFormat(ObfuscationItem item, string initialName, ulong index) {
            return string.Format("[SECURED-by-Z00bfuscator]-{0}-{1}", this.EncryptAsCaesar(initialName, 1), index);
        }

        string EncryptAsCaesar(string value, int shift) {
            char[] buffer = value.ToCharArray();
            char letter;
            for (int i = 0; i < buffer.Length; i++) {
                letter = buffer[i];
                letter = (char)(letter + shift);
                if (letter > 'z') {
                    letter = (char)(letter - 26);
                } else if (letter < 'a') {
                    letter = (char)(letter + 26);
                }
                buffer[i] = letter;
            }
            return new string(buffer);
        }

        void AddToXMLMap(ObfuscationItem item, string initialName, String obfuscated) {
            XmlElement element = this.m_xmlDocument.CreateElement("mapping");
            this.m_xmlElement.AppendChild(element);
            element.SetAttribute("Type", item.ToString());
            element.SetAttribute("InitialValue", initialName);
            element.SetAttribute("ObfuscatedValue", obfuscated);
        }

        #endregion

    }
}
