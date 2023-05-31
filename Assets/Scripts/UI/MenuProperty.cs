using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MenuProperty : MonoBehaviour
    {
        public MainMenuController mainMenuController;
        protected virtual void OnEnable()
        {
            mainMenuController.AddToUIStack(gameObject);
        }
    }
}

