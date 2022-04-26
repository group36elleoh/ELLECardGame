using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    [SerializeField] AudioClip correctAnswer;
    [SerializeField] AudioClip wrongAnswer;
    [SerializeField] AudioClip gameOver;

    // Source is constantly playing the background music
    private AudioSource audioSource;

    // other sound ideas
    // buy theme
    // multiplier increase

    public List<GameObject> voices;

    // Start is called before the first frame update
    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        voices = new List<GameObject>();

        if (FindObjectsOfType<SoundPlayer>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = PlayerPrefsManager.GetMusicVolume();
    }

    public void PlayCorrectAnswerSFX()
    {
        AudioSource.PlayClipAtPoint(correctAnswer, Camera.main.transform.position, PlayerPrefsManager.GetSFXVolume());
    }

    public void PlayWrongAnswerSFX()
    {
        AudioSource.PlayClipAtPoint(wrongAnswer, Camera.main.transform.position, PlayerPrefsManager.GetSFXVolume());
    }

    public void PlayGameOverSFX()
    {
        AudioSource.PlayClipAtPoint(gameOver, Camera.main.transform.position, PlayerPrefsManager.GetSFXVolume());
    }
    
    // Functions for card voices
    public void AddVoice(AudioClip audioClip)
    {
        DeleteVoices();

        GameObject newObj = new GameObject("Voice Sound");
        voices.Add(newObj);

        newObj.AddComponent<AudioSource>();
        newObj.transform.position = Camera.main.transform.position;
        AudioSource newObjAudioSource = newObj.GetComponent<AudioSource>();
        newObjAudioSource.clip = audioClip;
        newObjAudioSource.volume = PlayerPrefsManager.GetVoiceVolume();
        newObjAudioSource.Play();
    }

    private void DeleteVoices()
    {
        for (int i = 0; i < voices.Count; i++)
        {
            Destroy(voices[i]);
        }
        voices.Clear();
    }
   
}
