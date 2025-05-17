using UnityEngine.Networking;

public class BypassCertificateHandler : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        // 모든 인증서 무시 (테스트용에서만 사용!)
        return true;
    }
}
