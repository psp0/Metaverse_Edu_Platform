using UnityEngine.Networking;

public class BypassCertificateHandler : CertificateHandler
{
    protected override bool ValidateCertificate(byte[] certificateData)
    {
        // ��� ������ ���� (�׽�Ʈ�뿡���� ���!)
        return true;
    }
}
