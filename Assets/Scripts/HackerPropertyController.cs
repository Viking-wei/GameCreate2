using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HackerProperty
{
    None,
    Unlocked,
    End
};

public class HackerPropertyController : MonoBehaviour
{
    public HackerProperty hackerProperty;
    public List<GameObject> thePortal;
}
