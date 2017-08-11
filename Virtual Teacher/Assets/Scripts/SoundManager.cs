using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;



public class SoundManager : MonoBehaviour {

    public AudioClip ballBreak;
    public AudioClip ballWoop;

    public LanguageSound languageSounds;
    public int currentLanguage;

    public void Start()
    {
        currentLanguage = PlayerPrefs.GetInt("Language");
        GameObject go = Resources.Load(currentLanguage.ToString()) as GameObject;
        languageSounds = go.GetComponent<LanguageSound>();       
    }

    public void PlaySound()
    {
        GetComponent<AudioSource>().clip = languageSounds.play;
        GetComponent<AudioSource>().Play();
    }

    public void DownloadSound()
    {
        GetComponent<AudioSource>().clip = languageSounds.download;
        GetComponent<AudioSource>().Play();
    }

    public void CorrectSound()
    {
        GetComponent<AudioSource>().clip = languageSounds.correct;
        GetComponent<AudioSource>().Play();
    }
    public void WrongSound()
    {
        GetComponent<AudioSource>().clip = languageSounds.wrong;
        GetComponent<AudioSource>().Play();
    }
    public void HappySound()
    {
        GetComponent<AudioSource>().clip = languageSounds.happy;
        GetComponent<AudioSource>().Play();
    }
    public void NumberSound(int number)
    {
        GetComponent<AudioSource>().clip = languageSounds.numbers[number - 1];
        GetComponent<AudioSource>().Play();
    }
    public void ColorSound(int number)
    {
        GetComponent<AudioSource>().clip = languageSounds.colors[number - 1];
        GetComponent<AudioSource>().Play();
    }
    public void ZnakSound(int number)
    {
        GetComponent<AudioSource>().clip = languageSounds.znaki[number - 1];
        GetComponent<AudioSource>().Play();
    }

    public void BallBreakSound()
    {
        GetComponent<AudioSource>().clip = ballBreak;
        GetComponent<AudioSource>().Play();
    }

    public void BallWoopSound()
    {
        GetComponent<AudioSource>().clip = ballWoop;
        GetComponent<AudioSource>().Play();
    }
}
