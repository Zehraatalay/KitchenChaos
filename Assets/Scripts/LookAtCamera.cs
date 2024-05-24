using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAtCamera : MonoBehaviour
{
    private enum Mode
    {
        LookAt,
        LookAtInverted,
        CameraForward,
        CameraForwardInverted,
    }

    [SerializeField] private Mode mode;

    private void LateUpdate()
    {
        switch (mode)
        {
            case Mode.LookAt://nesne kameraya bakar 
                transform.LookAt(Camera.main.transform);
                break;
            case Mode.LookAtInverted://nesne kameradan ters yöne bakar 
                Vector3 dirFromCamera = transform.position - Camera.main.transform.position;//object'in kameraya olan  yönü hesaplanýr
                transform.LookAt(transform.position + dirFromCamera);//object'in pozisyonuna bu yön eklenerek nesnenin bakacagý hedef belirlenir 
                break;
            case Mode.CameraForward:
                transform.forward = Camera.main.transform.forward;
                break;
            case Mode.CameraForwardInverted:
                transform.forward = -Camera.main.transform.forward;
                break;
        }

       
    }

}
