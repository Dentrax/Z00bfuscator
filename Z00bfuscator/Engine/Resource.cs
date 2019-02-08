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

        #region DoObfuscateResource

        protected override void DoObfuscateResource(Resource resource) {
            if (!IsResourceObfuscatable(resource.Name))
                return;

            string resourceName = resource.Name.Substring(0, resource.Name.Length - 10);

            if (!m_mapResources.ContainsKey(resourceName))
                return;

            string obfucatedName = m_mapResources[resourceName];
            resource.Name = obfucatedName + ".resources";
        }

        public static bool IsResourceObfuscatable(string name) {
            bool flag = true;

            if (name == "<Module>")
                flag = false;

            return flag;
        }

        #endregion

    }
}
