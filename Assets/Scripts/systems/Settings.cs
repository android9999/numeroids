using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
public class Settings : MonoBehaviour
{
    [SerializeField]
    AudioMixer musicMixer;

    [SerializeField]
    AudioMixer soundsMixer;

    [SerializeField]
    GameObject settingsPanel;

    public void SetVolume(float volume)
    {
        musicMixer.SetFloat("volume", volume);
    }

    public void SetSoundsVolume(float volume)
    {
        soundsMixer.SetFloat("volume", volume);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ToggleSettingsPanel(bool state)
    {
        settingsPanel.SetActive(state);
    }
}
