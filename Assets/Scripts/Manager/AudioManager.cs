﻿using System;
using UnityEngine.Audio;
using System.Collections;
using UnityEngine;

public class AudioManager : MonoBehaviour {

    public Sound[] sounds;
    private bool keepFadeIn;
    private bool keepFadeOut;

    public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        //checks and sets singelton
        if (instance == null) {
            instance = this;
        }
        else {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        
        //goes though every added sound, add a soundSource and set all parameters
        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    //play the audio with the according name
    public void Play(string name)
    {
        Sound s;
        if ((s = GetSound(name)) == null) {
            return;
        }
        s.source.Play();
    }

    public void Pause(string name)
    {
        Sound s;
        if ((s = GetSound(name)) == null) {
            return;
        }
        s.source.Pause();
    }

    //stops the audio with the according name
    public void Stop(string name)
    {
        Sound s;
        if ((s = GetSound(name)) == null) {
            return;
        }
        s.source.Stop();
    }

    //stops all audio clips
    public void StopAll()
    {
        foreach (Sound s in sounds) {
            Stop(s.name);
        }
    }


    public void SetVolume(string name, float volume)
    {
        Sound s;
        if ((s = GetSound(name)) == null) {
            return;
        }

        Mathf.Clamp(volume, 0, 1f);
        s.volume = volume;
        s.source.volume = volume;
    }

    //returns if an specific audio clip is running
    public bool CheckIfAudioIsRunning(string name)
    {
        Sound s;
        if ((s = GetSound(name)) == null) {
            return false;
        }
        return s.source.isPlaying;
    }

    

    //fades audio in
    public IEnumerator FadeIn(string name, float speed)
    {
        keepFadeIn = true;
        keepFadeOut = false;

        Sound s = GetSound(name);
        
        float maxVolume = s.source.volume;
        s.source.volume = 0f;
        float currentVolume = 0f;

        Play(s.name);

        while (currentVolume < maxVolume && keepFadeIn) {
            currentVolume += speed;
            s.source.volume = currentVolume;
            yield return new WaitForSeconds(0.1f);
        }

        keepFadeIn = false;

    }

    public IEnumerator FadeOut(string name, float speed)
    {
        keepFadeIn = false;
        keepFadeOut = true;

        Sound s = GetSound(name);
        float currentVolume = s.source.volume;

        while (currentVolume > 0 && keepFadeOut) {
            currentVolume -= speed;
            s.source.volume = currentVolume;
            yield return new WaitForSeconds(0.1f);
        }

        Stop(s.name);

        keepFadeOut = false;

    }

    //get sound by name from array
    Sound GetSound(string name)
    {
        //find sound
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        //check if sound exists
        if (s == null) {
            Debug.LogWarning("Sound " + name + " not found!");
            return null;
        }

        return s;
    }


}
