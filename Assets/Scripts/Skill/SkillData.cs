using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Profiling;

[CreateAssetMenu(fileName = "SkillManager", menuName = "ScriptableObjects/SkillManager", order = 1)]
public class SkillManager : ScriptableObject
{
    [SerializeField] string sheetUrl = "";
    [SerializeField] SkillData _skillData = new();

    public string SheetUrl => sheetUrl;

    public async UniTask LoadGSS(string sheetUrl)
    {
        var request = UnityWebRequest.Get(sheetUrl);

        await request.SendWebRequest();

        if (request.error != null)
        {
            Debug.Log(request.error);
        }
        else
        {
            _skillData = JsonUtility.FromJson<SkillData>(request.downloadHandler.text);
            foreach (var skill in _skillData.Data)
            {
                Debug.Log("IdÅF" + skill.Id + "ÅANameÅF" + skill.SkillName + "Effect : " + skill.Effect + "CostMin : " + skill.CostMin + "CostMax : " + skill.CostMax);
            }
        }
    }

}

[System.Serializable]
public class SkillData
{
    public Skill[] Data;
}

[System.Serializable]
public class Skill
{
    public int Id;
    public string SkillName = "";
    public string Effect = "";
    public int CostMin;
    public int CostMax;
}