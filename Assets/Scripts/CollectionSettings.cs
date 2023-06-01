using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectionSettings : MonoBehaviour
{
    public string collectionName;

    private void OnDestroy()
    {
        if (collectionName.Equals("Parser code fragment"))
            GameManager.Instance.parserCodeFragmentNum++;
        else
            GameManager.Instance.encryptionAlgorithmCodeFragmentNum++;
    }
}
