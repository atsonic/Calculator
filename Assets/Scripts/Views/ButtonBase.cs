using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using Views;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Views
{
    public class ButtonBase : Button
    {
        [SerializeField] public string _buttonId;

        public string ButtonId
        {
            get { return _buttonId; }
            set { _buttonId = value; }
        }
        protected override void Start() {
            this.GetComponentInChildren<Text>().text = _buttonId;
        }
    }
}

#if UNITY_EDITOR
[CustomEditor(typeof(ButtonBase))]
public class ButtonBaseEditor : Editor
{
    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(serializedObject.FindProperty("_buttonId"));
        serializedObject.ApplyModifiedProperties();
    }
}
#endif