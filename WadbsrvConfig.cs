namespace wadbsrv
{
    public class WadbsrvConfig
    {
        public readonly int LocalPort;
        public readonly string PfxCertificatePath;
        public readonly string PfxPassword;
        public readonly string LocalIpAddress;
        public readonly bool SuppressCertificateErrors;
        public readonly string SQLiteDatabasePath;
        public readonly bool DebuggingEnabled;

        public WadbsrvConfig(int localPort, string pfxCertificatePath, string pfxPassword, string localIpAddress, bool suppressCertificateErrors, string sqliteDatabasePath, bool debuggingEnabled)
        {
            LocalPort = localPort;
            PfxCertificatePath = pfxCertificatePath;
            PfxPassword = pfxPassword;
            LocalIpAddress = localIpAddress;
            SuppressCertificateErrors = suppressCertificateErrors;
            SQLiteDatabasePath = sqliteDatabasePath;
            DebuggingEnabled = debuggingEnabled;
        }
    }
}