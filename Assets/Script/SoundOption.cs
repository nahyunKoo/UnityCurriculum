using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundOption : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider BgmSlider;
    public Slider EffectSlider;

    public void setBGMVolume()
    {
        audioMixer.SetFloat("BGM", Mathf.Log10(BgmSlider.value) * 20);

    }

    public void setEffectVolume()
    {

        audioMixer.SetFloat("Effect", Mathf.Log10(EffectSlider.value) * 20);
    }
}
