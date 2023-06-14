using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using Newtonsoft.Json;
using UnityEngine.Serialization;
using UI;
public class CoffeeMakerController : MonoBehaviour
{
    //when coffee is done, this delegate is called
    
    [HideInInspector]
    public PlayableDirector playableDirector;
    private TimelineAsset _timelineAsset;
    public Action<CoffeeResult> CoffeeResultDelegate;
    private List<string> _coffeeResult = new List<string>();
    
    private void Awake()
    {
        playableDirector = GetComponent<PlayableDirector>();
        _timelineAsset = (TimelineAsset)playableDirector.playableAsset;

        playableDirector.stopped += OnPlayableDirectorStopped;
    }

    /// <summary>the method is called when the timeline is stopped</summary>
    private void OnPlayableDirectorStopped(PlayableDirector director)
    {
        foreach (var track in _timelineAsset.GetRootTracks())
        {
            track.muted = false;
        }
        
        //FIXME: Need to invoke a Delegate
        CoffeeResultDelegate?.Invoke(ProcessCoffeeResult(_coffeeResult));

        _coffeeResult.Clear();
    }

    private void Update()
    {
        //FIXME: This is a temporary solution to play the timeline
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            playableDirector.Play();
        }
    }

    /// <summary>the method is called when a button is selected in the UI</summary>
    ///<param name="muteTrackName">the name of the track to mute</param>
    public void OnSelected(string muteTrackName)
    {
        if (muteTrackName.Length==0)
        {
            playableDirector.Resume();
            return;
        }

        if (MuteTrack(muteTrackName))
        {
            playableDirector.Resume();
        }
        else
        {
            Debug.LogError($"{muteTrackName} not found");
        }
    }

    ///<summary>add the result of the coffee to the list</summary>
    public void AddCoffeeResult(string coffeeResult)
    {
        _coffeeResult.Add(coffeeResult);
    }

    private CoffeeResult ProcessCoffeeResult(List<string> coffeeResult)
    {
        if(coffeeResult.Count!=3)
        {
            Debug.LogError("coffee result is not 3");
            return CoffeeResult.Error;
        }
        
        string coffeeType=coffeeResult[0];
        string coffeeDepth=coffeeResult[1];
        string coffeeTaste=coffeeResult[2];
        Dictionary<string, float> coffeeResultDict=DialogText.GetResultConfig(GameManager.Paragraph);

        float coffeeTypeValue=coffeeResultDict[coffeeType];
        float coffeeDepthValue=coffeeResultDict[coffeeDepth];
        float coffeeTasteValue=coffeeResultDict[coffeeTaste];

        if(GameManager.Paragraph==1)
        {
            float result=coffeeTypeValue*(coffeeDepthValue+coffeeTasteValue)+coffeeTasteValue;

            if(result<9.9)
                return CoffeeResult.Bad;
            else if(result<13.2)
                return CoffeeResult.Normal;
            else
                return CoffeeResult.Good;
        }

        else if(GameManager.Paragraph==2)
        {
            float result=coffeeTypeValue*coffeeDepthValue-coffeeTasteValue;

            if(result<6.4)
                return CoffeeResult.Bad;
            else if(result<12)
                return CoffeeResult.Normal;
            else
                return CoffeeResult.Good;
        }
        else
        {
            //TODO: paragraph 3
            Debug.LogError("paragraph 3 not complete");
            return CoffeeResult.Error;
        }
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
        playableDirector.stopped-= OnPlayableDirectorStopped;
    }
}
public enum CoffeeResult{
    Bad,
    Normal,
    Good,
    Error}
