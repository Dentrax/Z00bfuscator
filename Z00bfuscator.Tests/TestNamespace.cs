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
    public sealed class TestNamespace : TestCase {
        [Fact]
        public void Test() {
            void TestOK(string @namespace, string name, TypeAttributes attributes) {
                TypeDefinition definition = new TypeDefinition(@namespace, name, attributes, new TypeReference(string.Empty, string.Empty, null, null));

                string message = this.GetAssertMessage("TestNamespace", "TestOK()", name, attributes.ToString(), true, false);

                Assert.True(Obfuscator.IsNamespaceObfuscatable(definition), message);
            }

            void TestFAIL(string @namespace, string name, TypeAttributes attributes) {
                TypeDefinition definition = new TypeDefinition(@namespace, name, attributes, new TypeReference(string.Empty, string.Empty, null, null));

                string message = this.GetAssertMessage("TestNamespace", "TestFAIL()", name, attributes.ToString(), false, true);

                Assert.False(Obfuscator.IsNamespaceObfuscatable(definition), message);
            }

            TestFAIL("x", "<Module>", TypeAttributes.Public);
            TestFAIL("x", "Resources", TypeAttributes.Public);
            TestFAIL("x", "", TypeAttributes.Public);
            TestFAIL("x", "___TEST__", TypeAttributes.Public);
            TestFAIL("x", "<Test>", TypeAttributes.Public);
            TestFAIL("x", "Test", TypeAttributes.SpecialName);
            TestFAIL("x", "Test", TypeAttributes.RTSpecialName);

            TestOK("x", "Test", TypeAttributes.Public);
            TestOK("x", "_Test_", TypeAttributes.Public);

        }
    }
}
