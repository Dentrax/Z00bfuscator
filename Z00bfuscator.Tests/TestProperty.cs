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
    public sealed class TestProperty : TestCase {
        [Fact]
        public void Test() {
            void TestOK(string name, PropertyAttributes attributes) {
                PropertyDefinition definition = new PropertyDefinition(name, attributes, new TypeReference(string.Empty, string.Empty, null, null));

                string message = this.GetAssertMessage("TestProperty", "TestOK()", name, attributes.ToString(), true, false);

                Assert.True(Obfuscator.IsPropertyObfuscatable(definition), message);
            }

            void TestFAIL(string name, PropertyAttributes attributes) {
                PropertyDefinition definition = new PropertyDefinition(name, attributes, new TypeReference(string.Empty, string.Empty, null, null));

                string message = this.GetAssertMessage("TestProperty", "TestFAIL()", name, attributes.ToString(), false, true);

                Assert.False(Obfuscator.IsPropertyObfuscatable(definition), message);
            }

            TestFAIL("test", PropertyAttributes.SpecialName);
            TestFAIL("test", PropertyAttributes.RTSpecialName);

            TestOK("Test", PropertyAttributes.HasDefault);
            TestOK("Test", PropertyAttributes.None);
            TestOK("Test", PropertyAttributes.Unused);
        }
    }
}
