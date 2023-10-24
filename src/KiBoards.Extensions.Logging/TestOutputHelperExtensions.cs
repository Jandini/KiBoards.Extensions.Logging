using Microsoft.Extensions.Logging;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using Xunit.Abstractions;

namespace KiBoards.Extensions.Logging
{
    internal static class TestOutputHelperExtensions
    {
        public static ITestCase GetTestCase(this ITestOutputHelper output)
            => (output.GetType().GetField("test", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(output) as ITest).TestCase;

        public static ITest GetTest(this ITestOutputHelper output)
            => (output.GetType().GetField("test", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(output) as ITest);

        public static string ComputeMD5(this string value) => BitConverter.ToString(MD5.Create().ComputeHash(Encoding.UTF8.GetBytes(value))).Replace("-", "").ToLower();      
    }
}
