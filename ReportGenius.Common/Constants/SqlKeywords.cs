namespace ReportGenius.Common.Constants
{
    public static class SqlKeywords
    {
        public static readonly string[] DangerousKeywords =
        {
            "DROP",
            "TRUNCATE",
            "ALTER",
            "CREATE DATABASE",
            "SHUTDOWN",
            "RESTORE",
            "BACKUP",
            "MERGE",
            "GRANT",
            "REVOKE",
            "DENY",
            "EXEC",
            "EXECUTE",
            "XP_",
            "SP_CONFIGURE"
        };

        public static readonly string[] ReadOnlyKeywords =
        {
            "SELECT",
            "WITH"
        };
    }
}
