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

namespace Z00bfuscator.Tests
{
    public sealed class TestField : TestCase
    {
        [Fact]
        public void Test()
        {
            void TestOK(string name, FieldAttributes attributes) {
                FieldDefinition definition = new FieldDefinition(name, attributes, new TypeReference(string.Empty, string.Empty, null, null));

                string message = this.GetAssertMessage("TestField", "TestOK()", name, attributes.ToString(), true, false);

                Assert.True(Obfuscator.IsFieldObfuscatable(definition), message);
            }

            void TestFAIL(string name, FieldAttributes attributes) {
                FieldDefinition definition = new FieldDefinition(name, attributes, new TypeReference(string.Empty, string.Empty, null, null));

                string message = this.GetAssertMessage("TestField", "TestFAIL()", name, attributes.ToString(), false, true);

                Assert.False(Obfuscator.IsFieldObfuscatable(definition), message);
            }

            TestFAIL("<Test>", FieldAttributes.Public);
            TestFAIL("<Test>", FieldAttributes.SpecialName);
            TestFAIL("Test", FieldAttributes.SpecialName);
            TestFAIL("Test", FieldAttributes.RTSpecialName);

            TestOK("Test", FieldAttributes.Public);
        }
    }
}
