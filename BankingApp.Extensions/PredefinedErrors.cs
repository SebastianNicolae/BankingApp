using BankingApp.Extensions.ApiResponse;

namespace BankingApp.Extensions
{
    internal static class PredefinedErrors
    {
        private const string _prefix = "ge_";

        public static class General
        {
            public static readonly PredefinedError UnhandledException =
                new($"{_prefix}g_001", "Unhandled internal exception");
        }
    }
}