using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrefsManager
{
    private const string MUSIC_KEY = "musicKey";
    private const string SFX_KEY = "sfxKey";
    private const string VOICE_KEY = "voiceKey";
    private const string SELECTED_THEME = "selectedTheme";
    private const string SELECTED_TEXTURE = "selectedTexture";
    private const string SELECTED_BACKGROUND_IMAGE = "selectedBackgroundImage";
    private const string SELECTED_CARD = "selectedCard";
    private const string CREDITS = "credits";
    private const string UNLOCKED_THEMES = "unlockedThemes";
    private const string UNLOCKED_CARDS = "unlockedCards";

    public static void SetMusicVolume(float volume)
    {
        PlayerPrefs.SetFloat(MUSIC_KEY, volume);
    }

    public static void SetSFXVolume(float volume)
    {
        PlayerPrefs.SetFloat(SFX_KEY, volume);
    }

    public static void SetVoiceVolume(float volume)
    {
        PlayerPrefs.SetFloat(VOICE_KEY, volume);
    }

    public static float GetMusicVolume()
    {
        return PlayerPrefs.GetFloat(MUSIC_KEY, -1);
    }

    public static float GetSFXVolume()
    {
        return PlayerPrefs.GetFloat(SFX_KEY, -1);
    }

    public static float GetVoiceVolume()
    {
        return PlayerPrefs.GetFloat(VOICE_KEY, -1);
    } 
    
    public static void SetSelectedCard(string selectedCard)
    {
        PlayerPrefs.SetString(SELECTED_CARD, selectedCard);

    }

    public static string GetSelectedCard()
    {
        return PlayerPrefs.GetString(SELECTED_CARD);
    }

    public static string GetSelectedBackgroundImage()
    {
        return PlayerPrefs.GetString(SELECTED_BACKGROUND_IMAGE);
    }

    public static void SetSelectedBackgroundImage(string selectedBackgroundImage)
    {
        PlayerPrefs.SetString(SELECTED_BACKGROUND_IMAGE, selectedBackgroundImage);

    }

    public static void SetCredits(int credits)
    {
        PlayerPrefs.SetInt(CREDITS, credits);
    }

    public static int GetCredits()
    {
        return PlayerPrefs.GetInt(CREDITS, 0);
    }

    public static void SetUnlockedThemes(string themeName)
    {
        PlayerPrefs.SetString(UNLOCKED_THEMES, GetUnlockedThemes() + themeName);
    }

    public static string GetUnlockedThemes()
    {
        return PlayerPrefs.GetString(UNLOCKED_THEMES);
    }
    
    public static void SetUnlockedCards(string cardName)
    {
        PlayerPrefs.SetString(UNLOCKED_CARDS, GetUnlockedCards() + cardName);
    }

    public static string GetUnlockedCards()
    {
        return PlayerPrefs.GetString(UNLOCKED_CARDS);
    }
    public static void clearAll()
    {
        PlayerPrefs.SetString(UNLOCKED_THEMES, null);
        PlayerPrefs.SetString(UNLOCKED_CARDS, null);
        PlayerPrefs.SetString(SELECTED_CARD, null);
        PlayerPrefs.SetString(SELECTED_BACKGROUND_IMAGE, null);
        PlayerPrefs.SetInt(CREDITS, 20000);
    }
}
