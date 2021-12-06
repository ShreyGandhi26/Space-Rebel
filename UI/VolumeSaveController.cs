using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSaveController : MonoBehaviour
{
    [SerializeField]
    private Slider masterVolumeSlider = null;
    //   [SerializeField]
    //private Slider backgroundVolumeSlider = null;

    private void Start()
    {
        LoadValues();
    }

    public void SaveButton()
    {
        float masterVolumeVal = masterVolumeSlider.value;
        //float backgroundVolumeVal = backgroundVolumeSlider.value;
        PlayerPrefs.SetFloat("mastervolumeValue", masterVolumeVal);
        //PlayerPrefs.SetFloat("bgmvolumeValue", backgroundVolumeVal);
        LoadValues();
    }

    void LoadValues()
    {
        float masterVolumeVal = PlayerPrefs.GetFloat("mastervolumeValue");
        //float backgroundVolumeVal = PlayerPrefs.GetFloat("bgmvolumeValue");
        masterVolumeSlider.value = masterVolumeVal;
        //backgroundVolumeSlider.value = backgroundVolumeVal;
        AudioListener.volume = masterVolumeVal;
    }
}
