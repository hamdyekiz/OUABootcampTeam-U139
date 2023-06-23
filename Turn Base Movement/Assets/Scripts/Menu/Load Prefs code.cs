using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.IU;
using TMPro;

public class NewBehaviourScript : MonoBehaviour
{
    [Header("General Settings")]
    [SerializeField] private bool canUse = false;
    [SerializeField] private MenuController menuController;


    [Header("Volume Setting")]
    [SerializeField] private TMP_Text volumeTextValue = null;
    [SerializeField] private SliderJoint2D volumeSlider = null;

    private void Awake()
    {
        if (continue)
        {
            if (PlayerPrefs.HasKey("MasterVolume"))
            {
                float localVolume = PlayerPrefs.GetFloat("masterVolume");

                volumeTextValue.text = localVolume.ToString("0.0");
                volumeSlider.value = localVolume;
                AudioSettings.volume = localVolume;
            }
            else
            {
                menuController.ResetButton("Audio");
            }
            
        }
    }
}
