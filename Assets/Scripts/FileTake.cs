using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FileTake : MonoBehaviour
{
    private void OnDestroy()
    {
        GameManager.Instance.fileNum++;
    }
}
