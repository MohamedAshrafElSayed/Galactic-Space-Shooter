using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "GameData/Achievement")]
public class AchievementData : ScriptableObject
{
    [SerializeField] private string _achievementName;
    [SerializeField] private int _achievementScore;  

    public string AchievementName => _achievementName;
    public int AchievementScore => _achievementScore;
}
