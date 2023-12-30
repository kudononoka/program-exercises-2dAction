using UnityEditor;
using UnityEngine;

/// <summary>ボタンを押したらシートの読み込みをするようにした</summary>
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

             await _target.LoadSpreadSheetData(_target.SheetUrl);
        }

        if(_target != null && _target.IsLoading)
        {
            GUILayout.Label("読み込み中");
        }
    }
}
