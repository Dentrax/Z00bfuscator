#region License
// ====================================================
// Z00bfuscator Copyright(C) 2013-2019 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using Mono.Cecil;

namespace Z00bfuscator
{
    public sealed partial class Obfuscator : ObfuscatorBase {

        #region DoObfuscateType

        protected override void DoObfuscateType(TypeDefinition type) {
            if (!IsTypeObfuscatable(type))
                return;

            if (m_excludedTypes.Contains(type.FullName))
                return;

            string initialTypeName = type.FullName;
            type.Name = this.DoObfuscateItem(ObfuscationItem.Type, type.Name);

            if (!initialTypeName.Contains("/")) {
                this.m_mapResources.Add(initialTypeName, type.FullName);
            }

            foreach (AssemblyDefinition assembly in m_assemblyDefinitions)
                foreach (ModuleDefinition module in assembly.Modules)
                    foreach (TypeReference typeReference in module.GetTypeReferences())
                        if (typeReference.FullName == initialTypeName)
                            typeReference.Name = type.Name;

            foreach (MethodDefinition method in type.Methods)
                this.DoObfuscateMethod(type, initialTypeName, method);

            foreach (PropertyDefinition property in type.Properties)
                this.DoObfuscateProperty(type, property);

            foreach (FieldDefinition field in type.Fields)
                this.DoObfuscateField(type, field);
        }

        public static bool IsTypeObfuscatable(TypeDefinition type) {
            bool flag = true;

            if (string.IsNullOrEmpty(type.Name))
                flag = false;

            if (type.Name == "<Module>")
                flag = false;

            if (type.IsRuntimeSpecialName)
                flag = false;

            if (type.IsSpecialName)
                flag = false;

            if (type.Name.Contains("Resources"))
                flag = false;

            if (type.Name.StartsWith("<"))
                flag = false;

            if (type.Name.Contains("__"))
                flag = false;

            return flag;
        }

        #endregion

    }
}
