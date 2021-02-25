using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;

    AudioManager audioM;
    string[] trackList = { "ingameMusic1", "ingameMusic2" };
    public string currentTrack = "";

    bool musicIsPaused = false;

    private void Awake()
    {
        if (instance != null) {
            Destroy(this);
            return;
        }
        instance = this;

        audioM = AudioManager.instance;
        audioM.StopAll();

        //start music
        PlayRandomTrack();

        DontDestroyOnLoad(this);
    }

    private void Update()
    {
        //if track has finished, play another one
        if (!musicIsPaused && !audioM.CheckIfAudioIsRunning(currentTrack)) {
            PlayRandomTrack();
        }
    }

    //sets music Player to pause 
    public void DisableMusic()
    {
        audioM.Pause(currentTrack);
        musicIsPaused = true;
    }

    //sets music Player to pause 
    public void EnableMusic()
    {
        audioM.Play(currentTrack);
        musicIsPaused = false;
    }

    void PlayRandomTrack()
    {
        //get ad different track from list then last one which was played
        int trackIndex;
        do {
            trackIndex = Random.Range(0, trackList.Length);
        } while (trackList[trackIndex] == currentTrack);

        //play and save track
        audioM.Play(trackList[trackIndex]);
        currentTrack = trackList[trackIndex];
    }
}
