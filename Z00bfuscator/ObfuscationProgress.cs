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
    public struct ObfuscationProgress {
        public ulong CurrentObfuscatedMethodID { get; set; }
        public ulong CurrentObfuscatedTypeID { get; set; }
        public ulong CurrentObfuscatedNamespaceID { get; set; }
        public ulong CurrentObfuscatedPropertyID { get; set; }
        public ulong CurrentObfuscatedFieldID { get; set; }
    }
}
