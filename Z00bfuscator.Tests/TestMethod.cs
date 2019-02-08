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
    public sealed class TestMethod : TestCase {
        [Fact]
        public void Test() {
            void TestOK(string name, MethodAttributes attributes) {
                MethodDefinition definition = new MethodDefinition(name, attributes, new TypeReference(string.Empty, string.Empty, null, null));

                string message = this.GetAssertMessage("TestMethod", "TestOK()", name, attributes.ToString(), true, false);

                Assert.True(Obfuscator.IsMethodObfuscatable(definition), message);
            }

            void TestFAIL(string name, MethodAttributes attributes) {
                MethodDefinition definition = new MethodDefinition(name, attributes, new TypeReference(string.Empty, string.Empty, null, null));

                string message = this.GetAssertMessage("TestMethod", "TestFAIL()", name, attributes.ToString(), false, true);

                Assert.False(Obfuscator.IsMethodObfuscatable(definition), message);
            }

            TestFAIL("", MethodAttributes.Public);
            TestFAIL("Main", MethodAttributes.Public);
            TestFAIL("Main", MethodAttributes.SpecialName);
            TestFAIL("Main", MethodAttributes.RTSpecialName);
            TestFAIL("<Test>", MethodAttributes.Public);
            TestFAIL("<Test>", MethodAttributes.Abstract);
            TestFAIL("Test", MethodAttributes.Abstract);
            TestFAIL("Test", MethodAttributes.Virtual);

            TestOK("Test", MethodAttributes.Private);
            TestOK("Test", MethodAttributes.Static);
            TestOK("Test", MethodAttributes.Public);
        }
    }
}
