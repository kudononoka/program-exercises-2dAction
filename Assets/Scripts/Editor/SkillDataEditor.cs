using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Profiling;

[CustomEditor(typeof(SkillManager))]
public class SkillDataEditor : Editor
{
    SkillManager _target;
    public override async void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        if (GUILayout.Button("Data OnLoad"))
        {
            if(_target == null) _target = target as SkillManager;

             await _target.LoadGSS(_target.SheetUrl);
        }
    }
}
