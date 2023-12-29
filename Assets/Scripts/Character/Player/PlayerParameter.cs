using UnityEngine;

/// <summary>Playerのパラメーター</summary>
public class PlayerParameter
{ 
　　[Tooltip("現在のLevel")]
    int _currentLevel;

    [Tooltip("現在の経験値")]
    int _experiencePoint;

    [Tooltip("現在のHp")]
    int _cuurentHp;

    [Tooltip("現在の攻撃力")]
    int _attackPower;

    public int ExperiencePoint => _experiencePoint;
    public int AttackPower => _attackPower;

    public int CurrentHp => _cuurentHp;

    /// <summary>現在のHpの値を変えるメソッド</summary>
    /// <param name="hp">変えるHpの値</param>
    public void ChangeHp(int hp)
    {
        _cuurentHp = hp;
    }

    /// <summary>現在の攻撃力の値を変えるメソッド</summary>
    /// <param name="hp">変える攻撃力の値</param>
    public void ChangeAttackPower(int attackPower)
    {
        _attackPower = attackPower;
    }

    /// <summary>現在のLevelの値を変えるメソッド</summary>
    /// <param name="hp">変えるLevelの値</param>
    public void ChangeLevel(int level)
    {
        _currentLevel = level;
    }

    /// <summary>現在のHpの値を変えるメソッド</summary>
    /// <param name="hp">変える経験値</param>
    public void ChangeExperiencePoint(int point)
    {
        _experiencePoint = point;
    }
}
