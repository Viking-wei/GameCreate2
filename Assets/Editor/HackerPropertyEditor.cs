using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace Editor
{
    [CustomEditor(typeof(HackerPropertyController))]
    public class HackerPropertyEditor : UnityEditor.Editor
    {
        private SerializedObject _hackerPropertyController;
        
        private SerializedProperty _targetPlaneIndex;
        private SerializedProperty _hackerProperty;
        private SerializedProperty _thePortalList;

        private void OnEnable()
        {
            _hackerPropertyController = new SerializedObject(target);
            
            _targetPlaneIndex= _hackerPropertyController.FindProperty("targetPlaneIndex");
            _hackerProperty = _hackerPropertyController.FindProperty("hackerProperty");
            _thePortalList = _hackerPropertyController.FindProperty("thePortalList");
        }

        public override void OnInspectorGUI()
        {
            _hackerPropertyController.Update();
            
            EditorGUILayout.PropertyField(_targetPlaneIndex);
            EditorGUILayout.PropertyField(_hackerProperty);
            if(_hackerProperty.enumValueIndex==1)
                EditorGUILayout.PropertyField(_thePortalList);
            
            _hackerPropertyController.ApplyModifiedProperties();
        }
    }
}

