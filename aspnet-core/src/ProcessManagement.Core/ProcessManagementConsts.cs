using ProcessManagement.Debugging;

namespace ProcessManagement
{
    public class ProcessManagementConsts
    {
        public const string LocalizationSourceName = "ProcessManagement";

        public const string ConnectionStringName = "Default";

        public const bool MultiTenancyEnabled = true;


        /// <summary>
        /// Default pass phrase for SimpleStringCipher decrypt/encrypt operations
        /// </summary>
        public static readonly string DefaultPassPhrase =
            DebugHelper.IsDebug ? "gsKxGZ012HLL3MI5" : "25416183a76c4bf49c5a54d1cab4826e";
    }
}
