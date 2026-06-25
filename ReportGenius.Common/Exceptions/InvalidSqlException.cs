namespace ReportGenius.Common.Exceptions
{
    public sealed class InvalidSqlException : Exception
    {
        public InvalidSqlException(string message): base(message)
        {
        }
    }
}
