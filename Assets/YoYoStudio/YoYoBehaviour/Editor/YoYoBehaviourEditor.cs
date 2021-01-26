using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace YoYoStudio.Behaviour
{
    [CanEditMultipleObjects]
    [CustomEditor(typeof(YoYoBehaviour), true)]
    public class YoYoBehaviourEditor : EditorBase
    {
        bool drawDefaultInspector = true;
        protected virtual void OnEnable()
        {
            SetTitle(target.GetType().Name);
            SetMonoScript(MonoScript.FromMonoBehaviour((MonoBehaviour)target));
        }
        //*************************************
        protected void DisableDefaultInspector()
        {
            drawDefaultInspector = false;
        }
        //*************************************
        private static readonly string[] _dontIncludeMe = new string[] { "m_Script" };
        public override void OnInspectorGUI()
        {
            DrawLogoGUI();

            if (drawDefaultInspector)
            {
                serializedObject.Update();
                DrawPropertiesExcluding(serializedObject, _dontIncludeMe);
                serializedObject.ApplyModifiedProperties();
            }
        }
    }
}
