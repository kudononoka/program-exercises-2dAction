using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>スキルデータをスプレッドシートから読み込んで管理するクラス</summary>
[CreateAssetMenu(fileName = "SkillManager", menuName = "ScriptableObjects/SkillManager", order = 1)]
public class SkillManager : ScriptableObject
{
    [SerializeField]
    [Header("スプレッドシートのURL")]
    string sheetUrl = "";

    [SerializeField]
    [Header("スキルデータ")]
    SkillData _skillData = new();

    [Tooltip("ローディング中かどうか")]
    bool _isLoading = false;

    public string SheetUrl => sheetUrl;

    public bool IsLoading => _isLoading;

    /// <summary>スプレッドシートから取得して配列に格納する処理を行う</summary>
    /// <param name="sheetUrl">URL</param>
    /// <returns></returns>
    public async UniTask LoadSpreadSheetData(string sheetUrl)
    {
        _isLoading = true;
        var request = UnityWebRequest.Get(sheetUrl);

        await request.SendWebRequest();

        _isLoading = false;
        if (request.error != null)
        {
            Debug.Log(request.error);
        }
        else
        {
            _skillData = JsonUtility.FromJson<SkillData>(request.downloadHandler.text);
            foreach (var skill in _skillData.Data)
            {
                Debug.Log("Id：" + skill.Id + "、Name：" + skill.SkillName + "Effect : " + skill.Effect + "CostMin : " + skill.CostMin + "CostMax : " + skill.CostMax);
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