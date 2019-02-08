#region License
// ====================================================
// Z00bfuscator Copyright(C) 2013-2019 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace Z00bfuscator
{
    public struct ObfuscationInfo
    {
        public string OutputDirectory { get; set; }
        public bool ObfuscateTypes { get; set; }
        public bool ObfuscateMethods { get; set; }
        public bool ObfuscateNamespaces { get; set; }
        public bool ObfuscateProperties { get; set; }
        public bool ObfuscateFields { get; set; }
        public bool ObfuscateResources { get; set; }

        public ObfuscationInfo(string outputDirectory, bool obfuscateTypes, bool obfuscateMethods, bool obfuscateNamespaces, bool obfuscateProperties, bool obfuscateFields, bool obfuscateResources) : this() {
            this.OutputDirectory = outputDirectory;
            this.ObfuscateTypes = obfuscateTypes;
            this.ObfuscateMethods = obfuscateMethods;
            this.ObfuscateNamespaces = obfuscateNamespaces;
            this.ObfuscateProperties = obfuscateProperties;
            this.ObfuscateFields = obfuscateFields;
            this.ObfuscateResources = obfuscateResources;
        }
    }
}
