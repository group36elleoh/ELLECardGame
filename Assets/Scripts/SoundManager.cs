using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;
    [SerializeField] Slider voiceSlider;

    [SerializeField] InputField musicInputField;
    [SerializeField] InputField sfxInputField;
    [SerializeField] InputField voiceInputField;

    [Range(0, 1)] [SerializeField] float MUSIC_DEFAULT = 0.5f;
    [Range(0, 1)] [SerializeField] float SFX_DEFAULT = 0.5f;
    [Range(0, 1)] [SerializeField] float VOICE_DEFAULT = 0.5f;

    private const float MIN_VOLUME_VALUE = 0;
    private const float MAX_VOLUME_VALUE = 100;

    private float musicVolume;
    private float sfxVolume;
    private float voiceVolume;

    private bool canChangeSliderOrField = true;

    // Start is called before the first frame update
    void Start()
    {
        // Get current volume values from PlayerPrefs. If no values are saved, use the default values
        musicVolume = PlayerPrefsManager.GetMusicVolume();
        if (musicVolume == -1)
            musicVolume = MUSIC_DEFAULT;

        sfxVolume = PlayerPrefsManager.GetSFXVolume();
        if (sfxVolume == -1)
            sfxVolume = SFX_DEFAULT;

        voiceVolume = PlayerPrefsManager.GetVoiceVolume();
        if (voiceVolume == -1)
            voiceVolume = VOICE_DEFAULT;

        // Set slider and input field values
        musicSlider.value = musicVolume * 100;
        sfxSlider.value = sfxVolume * 100;
        voiceSlider.value = voiceVolume * 100;
        
        musicInputField.text = (musicVolume * 100).ToString();
        sfxInputField.text = (sfxVolume * 100).ToString();
        voiceInputField.text = (voiceVolume * 100).ToString();
    }

    // Wait a few milliseconds before allowing a slider or field value to change again
    // Prevents the UpdateFromSlider and UpdateFromField functions from repeatedly calling each other
    private IEnumerator SliderFieldCooldown()
    {
        yield return new WaitForSeconds(0.1f);
        canChangeSliderOrField = true;
    }


    // When slider is changed, change the field text and save the value with PlayerPrefs
    public void UpdateMusicFromSlider()
    {
        if (!canChangeSliderOrField)
            return;

        canChangeSliderOrField = false;
        StartCoroutine("SliderFieldCooldown");
        musicInputField.text = musicSlider.value.ToString();
        PlayerPrefsManager.SetMusicVolume(musicSlider.value / 100);
    }

    // When field is changed, change the slider value and save the value with PlayerPrefs
    public void UpdateMusicFromField()
    {
        if (!canChangeSliderOrField)
            return;

        canChangeSliderOrField = false;
        StartCoroutine("SliderFieldCooldown");
        if (musicInputField.text.Equals(""))
        {
            musicInputField.text = musicSlider.value.ToString();
            return;
        }

        float f = ConvertToFloat(musicInputField.text);
        if (f == -1)
        {
            musicInputField.text = musicSlider.value.ToString();
        }
        else
        {
            musicSlider.value = f;
            PlayerPrefsManager.SetMusicVolume(f / 100);
        }
    }

    // When slider is changed, change the field text and save the value with PlayerPrefs
    public void UpdateSFXFromSlider()
    {
        if (!canChangeSliderOrField)
            return;

        canChangeSliderOrField = false;
        StartCoroutine("SliderFieldCooldown");
        sfxInputField.text = sfxSlider.value.ToString();
        PlayerPrefsManager.SetSFXVolume(sfxSlider.value / 100);
    }

    // When field is changed, change the slider value and save the value with PlayerPrefs
    public void UpdateSFXFromField()
    {
        if (!canChangeSliderOrField)
            return;

        canChangeSliderOrField = false;
        StartCoroutine("SliderFieldCooldown");
         
        if (sfxInputField.text.Equals(""))
        {
            sfxInputField.text = sfxSlider.value.ToString();
            return;
        }

        float f = ConvertToFloat(sfxInputField.text);
        if (f == -1)
        {
            sfxInputField.text = sfxSlider.value.ToString();
        }
        else
        {
            sfxSlider.value = f;
            PlayerPrefsManager.SetSFXVolume(f / 100);
        }
    }

    // When slider is changed, change the field text and save the value with PlayerPrefs
    public void UpdateVoiceFromSlider()
    {
        if (!canChangeSliderOrField)
            return;

        canChangeSliderOrField = false;
        StartCoroutine("SliderFieldCooldown");
        voiceInputField.text = voiceSlider.value.ToString();
        PlayerPrefsManager.SetVoiceVolume(voiceSlider.value / 100);
    }

    // When field is changed, change the slider value and save the value with PlayerPrefs
    public void UpdateVoiceFromField()
    {
        if (!canChangeSliderOrField)
            return;

        canChangeSliderOrField = false;
        StartCoroutine("SliderFieldCooldown");
        if (voiceInputField.text.Equals(""))
        {
            voiceInputField.text = voiceSlider.value.ToString();
            return;
        }

        float f = ConvertToFloat(voiceInputField.text);
        if (f == -1)
        {
            voiceInputField.text = voiceSlider.value.ToString();
        }
        else
        {
            voiceSlider.value = f;
            PlayerPrefsManager.SetVoiceVolume(f / 100);
        }
    }
    
    // Reset the volume values to their defaults and save the values to PlayerPrefs
    public void RestoreDefaults()
    {
        musicSlider.value = MUSIC_DEFAULT * 100;
        sfxSlider.value = SFX_DEFAULT * 100;
        voiceSlider.value = VOICE_DEFAULT * 100;

        musicInputField.text = (MUSIC_DEFAULT * 100).ToString();
        sfxInputField.text = (SFX_DEFAULT * 100).ToString();
        voiceInputField.text = (VOICE_DEFAULT * 100).ToString();

        PlayerPrefsManager.SetMusicVolume(MUSIC_DEFAULT);
        PlayerPrefsManager.SetSFXVolume(SFX_DEFAULT);
        PlayerPrefsManager.SetVoiceVolume(VOICE_DEFAULT);
    }

    // Convert a string to a float, if the string can't be converted,
    // or the float value is out of range, return -1
    private float ConvertToFloat(string str)
    {
        float f;
        try
        {
            f = float.Parse(str);
        }
        catch
        {
            f = -1;
        }

        if (f < MIN_VOLUME_VALUE || f > MAX_VOLUME_VALUE)
            f = -1;

        return f;
    }
}
