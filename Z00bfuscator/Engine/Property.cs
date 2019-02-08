#region License
// ====================================================
// Z00bfuscator Copyright(C) 2013-2019 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using System;

using Mono.Cecil;

namespace Z00bfuscator
{
    public sealed partial class Obfuscator : ObfuscatorBase {

        #region DoObfuscateProperty

        protected override void DoObfuscateProperty(TypeDefinition type, PropertyDefinition property) {
            if (!IsPropertyObfuscatable(property))
                return;

            string initialName = property.Name;
            string obfuscatedName = DoObfuscateItem(ObfuscationItem.Property, property.Name);

            if (property.DeclaringType.Name == type.Name && property.DeclaringType.Namespace == type.Namespace) {
                if (!Object.ReferenceEquals(property, property) && (property.Name == property.Name) && (property.Parameters.Count == property.Parameters.Count)) {
                    bool paramsEquals = true;
                    for (int paramIndex = 0; paramIndex < property.Parameters.Count; paramIndex++)
                        if (property.Parameters[paramIndex].ParameterType.FullName != property.Parameters[paramIndex].ParameterType.FullName) {
                            paramsEquals = false;
                            break;
                        }
                    try {
                        if (paramsEquals)
                            property.Name = obfuscatedName;
                    } catch (InvalidOperationException) { }
                }
            }

            property.Name = obfuscatedName;
        }

        public static bool IsPropertyObfuscatable(PropertyDefinition property) {
            bool flag = true;

            if (property.IsSpecialName)
                flag = false;

            if (property.IsRuntimeSpecialName)
                flag = false;

            return flag;
        }

        #endregion
    }
}
