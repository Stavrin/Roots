using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    public AudioSource[] tracks;
    private int currentTrack;
    private int nextTrack;
    private bool changingTracks;

    // Start is called before the first frame update
    void Start()
    {
        if (tracks[0] == null || tracks.Length == 0)
        {
            return; // handle no tracks
        }
        tracks[0].Play();
        currentTrack = 0;
        nextTrack = -1;
        changingTracks = false;
    }

    // Handles changing when track has finished
    void Update()
    {
        if (!changingTracks || nextTrack == -1)
        {
            return;
        }
        if (tracks[currentTrack].time < 1)
        {
            tracks[nextTrack].Play();
            changingTracks = false;
            currentTrack = nextTrack;
            nextTrack = -1;
        }
    }

    // changes track after this one has ended
    void ChangeAfterCurrentTrack(int a_nextTrack)
    {
        changingTracks = true;
        nextTrack = a_nextTrack;
    }

    // stops current track and plays new one
    void PlayTrack(int a_track)
    {
        tracks[a_track].Play();
        currentTrack = a_track;
    }

    int GetCurrentTrack()
    {
        return currentTrack;
    }

}
