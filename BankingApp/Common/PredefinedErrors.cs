using BankingApp.Extensions.ApiResponse;

namespace BankingApp.Common
{
    public static class PredefinedErrors
    {
        private const string _prefix = "bankingApp_";

        public static class General
        {
            public static PredefinedError BadRequest =
                new($"{_prefix}g_001", "Invalid request", "Request doesn't contain expected payload");

            public static PredefinedError UnhandledException =
                new($"{_prefix}g_002", "Unhandled internal exception");
        }

        public static class Specific
        {
            public static PredefinedError AccountDoesntExist =
                new($"{_prefix}d_001", "Account doesn't exist");

            public static PredefinedError ClientDoesntExist =
                new($"{_prefix}c_001", "Client doesn't exist");

        }
    }
}
