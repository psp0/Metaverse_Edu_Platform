using UnityEngine;

public class BillboardUI : MonoBehaviour
{
    private Camera mainCam;

    void Start()
    {
        mainCam = Camera.main;
    }

    void LateUpdate()
    {
        if (mainCam != null)
            transform.LookAt(transform.position + mainCam.transform.forward);
    }
}
