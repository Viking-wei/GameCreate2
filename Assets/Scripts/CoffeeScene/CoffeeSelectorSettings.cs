using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CoffeeSelectorSettings : MonoBehaviour
{
    public PlayableDirector playableDirector;
    private void OnEnable()    
    {
        playableDirector.Pause();
    }
}