using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor (typeof (PlayerNavigation))]
public class DropdownEditor : Editor {

    public override void OnInspectorGUI () {

        PlayerNavigation script = (PlayerNavigation) target;

        GUIContent arrayLabel = new GUIContent ("CollectibleType");
        script.CollectibleType_Index = EditorGUILayout.Popup (arrayLabel, script.CollectibleType_Index, script.CollectibleType);

        GUIContent arrayLabel1 = new GUIContent ("CollectibleColor");
        script.CollectibleColor_Index = EditorGUILayout.Popup (arrayLabel1, script.CollectibleColor_Index, script.CollectibleColor);

        EditorGUILayout.Space ();
        EditorGUILayout.Space ();

        base.OnInspectorGUI ();
    }
}