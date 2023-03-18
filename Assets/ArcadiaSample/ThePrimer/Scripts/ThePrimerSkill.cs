using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewSkill", menuName = "Skills/Skill")]
[System.Serializable]
public class ThePrimerSkill : ScriptableObject
{
    public int Continues;
    public string skillName;
    public float skillFatigue;
    public int skillCycle;
    public int skillPoints;
    public int skillLevel;
    public Sprite skillIcon;
    public Color skillColor;
    public string LastUpdated;



}
