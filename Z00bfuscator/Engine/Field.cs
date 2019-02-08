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

        #region ObfuscateFields

        protected override void DoObfuscateField(TypeDefinition type, FieldDefinition field) {
            if (!IsFieldObfuscatable(field))
                return;

            string initialName = field.Name;
            string obfuscatedName = DoObfuscateItem(ObfuscationItem.Field, field.Name);

            if (field.DeclaringType.Name == type.Name && field.DeclaringType.Namespace == type.Namespace)
                if (!Object.ReferenceEquals(field, field) && (field.Name == initialName)) {
                    try {
                        field.Name = obfuscatedName;
                    } catch (InvalidOperationException) { }
                }

            field.Name = obfuscatedName;
        }

        public static bool IsFieldObfuscatable(FieldDefinition field) {
            bool flag = true;

            if (field.IsRuntimeSpecialName)
                flag = false;

            if (field.IsSpecialName)
                flag = false;

            if (field.Name.StartsWith("<"))
                flag = false;

            return flag;
        }

        #endregion

    }
}
