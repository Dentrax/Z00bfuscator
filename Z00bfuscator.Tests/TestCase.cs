#region License
// ====================================================
// Z00bfuscator Copyright(C) 2013-2019 Furkan Türkal
// This program comes with ABSOLUTELY NO WARRANTY; This is free software,
// and you are welcome to redistribute it under certain conditions; See
// file LICENSE, which is part of this source code package, for details.
// ====================================================
#endregion

namespace Z00bfuscator.Tests
{
    public abstract class TestCase
    {
        protected string GetAssertMessage(string testCaseName, string testFuncName, string name, string attributes, bool expected, bool actual) {
            return string.Format("[{0}::{1}]: Name: {2} - Attributes: {3} - Expected: {4} - Actual: {5}", testCaseName, testFuncName, name, attributes, expected, actual);
        }

        protected string PrintTestCase(string testCaseName, bool start) {
            if (start) {
                return string.Format("[{0}::Test]: Test case started!", testCaseName);
            }
            return string.Format("[{0}::Test]: Test case finished!", testCaseName);
        }
    }
}
