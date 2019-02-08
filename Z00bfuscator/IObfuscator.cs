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
    public interface IObfuscator {
        void AddAssembly(string path, bool obfuscate);
        void StartObfuscation();
    }
}
