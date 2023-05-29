using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class MainMenuController : MonoBehaviour
    {
        public GameObject mainMenu;
        private Stack<GameObject> _uiHierarchy;
        public void ShowMenu()
        {
            mainMenu.SetActive(true);
        }
        
        
    }
}

