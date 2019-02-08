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
    public abstract class ObfuscatorBase {
        protected abstract void AsyncStartObfuscation();

        protected abstract void UpdateProgress(string message, int percent);

        protected abstract void LogProgress(string message);

        protected abstract void DoObfuscateField(TypeDefinition type, FieldDefinition field);

        protected abstract void DoObfuscateMethod(TypeDefinition type, string initialTypeName, MethodDefinition method);

        protected abstract void DoObfuscateNamespace(TypeDefinition type);

        protected abstract void DoObfuscateProperty(TypeDefinition type, PropertyDefinition property);

        protected abstract void DoObfuscateResource(Resource resource);

        protected abstract void DoObfuscateType(TypeDefinition type);
    }
}
