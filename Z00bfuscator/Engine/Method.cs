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

        #region DoObfuscateMethod

        protected override void DoObfuscateMethod(TypeDefinition type, string initialTypeName, MethodDefinition method) {
            if (!IsMethodObfuscatable(method))
                return;

            string initialName = method.Name;
            string obfuscatedName = DoObfuscateItem(ObfuscationItem.Method, method.Name);

            if (method.DeclaringType.Name == type.Name && method.DeclaringType.Namespace == type.Namespace)

                if (!Object.ReferenceEquals(method, method) &&
                    method.Name == initialName &&
                    method.HasThis == method.HasThis &&
                    method.CallingConvention == method.CallingConvention &&
                    method.Parameters.Count == method.Parameters.Count &&
                    method.GenericParameters.Count == method.GenericParameters.Count &&
                    method.ReturnType.Name == method.ReturnType.Name &&
                    method.ReturnType.Namespace == method.ReturnType.Namespace
                    ) {
                    bool paramsEquals = true;
                    for (int paramIndex = 0; paramIndex < method.Parameters.Count; paramIndex++)
                        if (method.Parameters[paramIndex].ParameterType.FullName != method.Parameters[paramIndex].ParameterType.FullName) {
                            paramsEquals = false;
                            break;
                        }

                    for (int paramIndex = 0; paramIndex < method.GenericParameters.Count; paramIndex++)
                        if (method.GenericParameters[paramIndex].FullName != method.GenericParameters[paramIndex].FullName) {
                            paramsEquals = false;
                            break;
                        }

                    try {
                        if (paramsEquals)
                            method.Name = obfuscatedName;
                    } catch (InvalidOperationException) { }
                }

            method.Name = obfuscatedName;
        }

        public static bool IsMethodObfuscatable(MethodDefinition method) {
            bool flag = true;

            if (string.IsNullOrEmpty(method.Name))
                flag = false;

            if (method.IsConstructor)
                flag = false;

            if (method.IsRuntime)
                flag = false;

            if (method.IsRuntimeSpecialName)
                flag = false;

            if (method.IsSpecialName)
                flag = false;

            if (method.IsVirtual)
                flag = false;

            if (method.IsAbstract)
                flag = false;

            if (method.Overrides.Count > 0)
                flag = false;

            if (method.Name.StartsWith("<"))
                flag = false;

            if (method.Name.StartsWith("Main"))
                flag = false;

            return flag;
        }

        #endregion

    }
}
