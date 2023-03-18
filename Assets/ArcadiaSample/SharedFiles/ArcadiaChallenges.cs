using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewChallenge", menuName = "Challenges/Challenge")]
[System.Serializable]
public class ArcadiaChallenges : ScriptableObject
{
    
    public string gameName;
    public bool hasCompleted;
    public bool hasCollected;
    public string dateCompleted;
    public int spRewardAmount;
    public int tokenRewardAmount;
    public int xpRewardAmount;
    public int progress;
    public string challengeID;
    public string challengeName;

    public string challengeDescription;

    public Sprite skillIcon;
    public Color skillColor;
    public string LastUpdated;

    public ThePrimerSkill[] Skills = new ThePrimerSkill[1];

}
