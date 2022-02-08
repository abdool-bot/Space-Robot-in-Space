using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;

public class Options : MonoBehaviour
{
    [SerializeField] private CinemachineFreeLook freeLook;
    
    public void ShowNoSound(TMP_Text textToAlter)
    {
        if (textToAlter == null) return;
        textToAlter.text = "there is no sound in space.";
    }

    public void ChangeMouseSpeed(float value)
    {
        if(freeLook == null) return;

        freeLook.m_XAxis.m_MaxSpeed = value;
    }
}
