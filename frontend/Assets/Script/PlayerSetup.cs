using Photon.Pun;
using UnityEngine;
using Controller;

public class PlayerSetup : MonoBehaviourPun
{
    [SerializeField] private Camera playerCamera;

    void Start()
    {
        if (photonView.IsMine)
        {
            // �� ī�޶� Ȱ��ȭ
            playerCamera.gameObject.SetActive(true);

            // �±׸� MainCamera�� ���� (���� Main Camera�� �����ص� ��)
            playerCamera.tag = "MainCamera";
        }
        else
        {
            // �ٸ� ����� ī�޶�� ����
            playerCamera.gameObject.SetActive(false);
        }
    }
}
