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
            // 내 카메라만 활성화
            playerCamera.gameObject.SetActive(true);

            // 태그를 MainCamera로 지정 (기존 Main Camera는 삭제해도 됨)
            playerCamera.tag = "MainCamera";
        }
        else
        {
            // 다른 사람의 카메라는 꺼둠
            playerCamera.gameObject.SetActive(false);
        }
    }
}
