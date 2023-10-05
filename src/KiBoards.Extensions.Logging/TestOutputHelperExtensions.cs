using System.Reflection;
using Xunit.Abstractions;

namespace KiBoards.Extensions.Logging
{
    internal static class TestOutputHelperExtensions
    {
        public static ITestCase GetTestCase(this ITestOutputHelper output)
            => (output.GetType().GetField("test", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(output) as ITest).TestCase;

        public static ITest GetTest(this ITestOutputHelper output)
            => (output.GetType().GetField("test", BindingFlags.Instance | BindingFlags.NonPublic).GetValue(output) as ITest);

    }
}
