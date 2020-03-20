using System;
using System.Collections.Generic;
using System.Text;

namespace wadbsrv
{
    public class WadbsrvConfig
    {
        public readonly int LocalPort;
        public readonly string PfxCertificatePath;
        public readonly string PfxPassword;
        public readonly string LocalIpAddress;
        public readonly bool SuppressCertificateErrors;
        public WadbsrvConfig(int localPort, string pfxCertificatePath, string pfxPassword, string localIpAddress, bool suppressCertificateErrors)
        {
            LocalPort = localPort;
            PfxCertificatePath = pfxCertificatePath;
            PfxPassword = pfxPassword;
            LocalIpAddress = localIpAddress;
            SuppressCertificateErrors = suppressCertificateErrors;
        }
    }
}
