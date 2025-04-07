using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Scriptable Objects/Character")]
public class Character : ScriptableObject
{
    public string characterName;
    public List<ProficiencyLevel> proficiencies;
}

[System.Serializable]
public class ProficiencyLevel
{
    public ProficiencyType type;
    [Range(0, 3)] public int level; // for looser, good enough and perfection
}