using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.LookDev;
using UnityEngine;
using UnityEngine.InputSystem;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public GameObject mainMenu;
        private Stack<GameObject> _uiHierarchy;

        private void Awake()
        {
            _uiHierarchy = new Stack<GameObject>();
            Debug.Log("MainMenuController script has been called");
        }
        

        public void ProcessEsc(InputAction.CallbackContext context)
        {
            if (!context.started) return;

            if (_uiHierarchy.TryPop(out var poppedUI))
            {
                poppedUI.SetActive(false);
                Debug.Log(poppedUI.name + " has been popped from the stack");
                
                if (_uiHierarchy.TryPeek(out var a))
                {
                    a.SetActive(true);
                    Debug.Log(a.name + "is the current UI");
                }
            }
            else
                mainMenu.SetActive(true);
            
            Debug.Log(_uiHierarchy.Count.ToString());
        }

        public void ProcessContinue()
        {
            mainMenu.SetActive(false);
            _uiHierarchy.Pop();
            Debug.Log(_uiHierarchy.Count.ToString());
        }
        
        /// <summary>
        /// Add the game object to the UI Hierarchy stack
        /// </summary>
        public void AddToUIStack(GameObject theGameObject)
        {
            _uiHierarchy.Push(theGameObject);
        }
    }
}

