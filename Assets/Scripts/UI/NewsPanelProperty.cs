using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace UI
{
    public class NewsPanelProperty : MenuProperty
    {
        public NewsDirector newsDirector;
        protected override void OnEnable()
        {
            base.OnEnable();
            newsDirector.InitializedNewsList();
        }
    }
}