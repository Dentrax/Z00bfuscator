#region License
// ====================================================
// Z00bfuscator Copyright(C) 2013-2019 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

using Mono.Cecil;
using Xunit;

namespace Z00bfuscator.Tests {
    public sealed class TestResource : TestCase {
        [Fact]
        public void Test() {
            void TestOK(string name, ManifestResourceAttributes attributes) {
                string message = this.GetAssertMessage("TestResource", "TestOK()", name, attributes.ToString(), true, false);

                Assert.True(Obfuscator.IsResourceObfuscatable(name), message);
            }

            void TestFAIL(string name, ManifestResourceAttributes attributes) {
                string message = this.GetAssertMessage("TestResource", "TestFAIL()", name, attributes.ToString(), false, true);

                Assert.False(Obfuscator.IsResourceObfuscatable(name), message);
            }

            TestFAIL("<Module>", ManifestResourceAttributes.Private);

            TestOK("Test", ManifestResourceAttributes.Private);
            TestOK("Test", ManifestResourceAttributes.Public);
            TestOK("Test", ManifestResourceAttributes.VisibilityMask);
        }
    }
}
