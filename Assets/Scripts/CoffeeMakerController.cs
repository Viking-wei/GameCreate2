using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class CoffeeMakerController : MonoBehaviour
{
    //when coffee is done, this delegate is called
    static public Action<List<int>> coffeeResultDelegate;
    private List<int> _coffeeResult = new List<int>();
    private PlayableDirector _playableDirector;
    private TimelineAsset _timelineAsset;
    private void Awake()
    {
        _playableDirector = GetComponent<PlayableDirector>();
        _timelineAsset = (TimelineAsset)_playableDirector.playableAsset;

        _playableDirector.stopped += OnPlayableDirectorStopped;

        // foreach (var track in _timelineAsset.GetRootTracks())
        // {
        //     Debug.Log(track.name);
        // }
    }

    /// <summary>the method is called when the timeline is stopped</summary>
    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        foreach (var track in _timelineAsset.GetRootTracks())
        {
            track.muted = false;
        }
        coffeeResultDelegate?.Invoke(_coffeeResult);
    }

    private void Update()
    {
        //FIXME: This is a temporary solution to play the timeline
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            _playableDirector.Play();
        }
    }

    /// <summary>the method is called when a button is selected in the UI</summary>
    ///<param name="muteTrackName">the name of the track to mute</param>
    public void OnSelected(string muteTrackName)
    {
        if (muteTrackName.Length==0)
        {
            _playableDirector.Resume();
            return;
        }

        if (MuteTrack(muteTrackName))
        {
            _playableDirector.Resume();
        }
        else
        {
            Debug.LogError($"{muteTrackName} not found");
        }
    }

    ///<summary>add the result of the coffee to the list</summary>
    public void OnCoffeeResult(int coffeeResult)
    {
        _coffeeResult.Add(coffeeResult);
    }

    /// <summary> Mute the track with the given name </summary>
    private bool MuteTrack(string muteTrackName)
    {
        foreach (var track in _timelineAsset.GetRootTracks())
        {
            if (track.name == muteTrackName)
            {
                track.muted = true;
                return true;
            }
        }
        return false;
    }

    private void OnDestroy() 
    {
        _playableDirector.stopped-= OnPlayableDirectorStopped;
    }
}
