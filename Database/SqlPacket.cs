namespace wadbsrv.Database
{
    public class SqlPacket
    {
        public readonly object Data;
        public readonly bool Success;
        public readonly string ErrorMessage;

        private SqlPacket(object data, bool success, string errorMessage)
        {
            Data = data;
            ErrorMessage = errorMessage;
            Success = success;
        }

        public static SqlPacket Create(object data)
        {
            return new SqlPacket(data, true, string.Empty);
        }

        public static SqlPacket Create(object data, bool success, string errorMessage)
        {
            return new SqlPacket(data, success, errorMessage);
        }
    }
}