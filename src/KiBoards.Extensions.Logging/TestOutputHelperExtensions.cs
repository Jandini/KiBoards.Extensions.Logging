using Microsoft.Extensions.Logging;
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


        public static IDisposable BeginScope<T>(this ILogger<T> logger, ITestOutputHelper output)
        {
            var testCase = GetTestCase(output);

            return logger.BeginScope(new Dictionary<string, object>()
            {
                ["UniqueId"] = testCase.UniqueID,
            });
        }
    }
}
