using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "NewAttribute", menuName = "Attributes/Attribute")]
[System.Serializable]
public class ThePrimerAttribute : ScriptableObject
{
    public string attributeName;
    public float attributeFatigue;
    public int attributeCycle;
    public int attributePoints;
    public int attributeLevel;
    public Color attributeColor;
    public string LastUpdated;
    public string LastCalculated;

}
