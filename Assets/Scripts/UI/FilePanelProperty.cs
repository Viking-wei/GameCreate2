using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI
{
    public class FilePanelProperty : MenuProperty
    {
        public FileDirector fileDirector;
        protected override void OnEnable()
        {
            base.OnEnable();
            fileDirector.InitializedNewsList();
        }
    }
}

