using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCam : MonoBehaviour
{
    public CinemachineVirtualCamera vcam;
#pragma warning disable 649
    [SerializeField] float screenY;
    [SerializeField] float screenX;
    [SerializeField] Transform m_Follow;
#pragma warning restore 649
    void OnTriggerEnter2D(Collider2D col)
    {
        if (screenY != 0)
        {
            vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenY = screenY;
        }
        if (screenX != 0)
        {
            vcam.GetCinemachineComponent<CinemachineFramingTransposer>().m_ScreenX = screenX;
        }
        if (m_Follow != null)
        {
            vcam.GetComponent<CinemachineVirtualCamera>().Follow = m_Follow;
        }
    }
}
