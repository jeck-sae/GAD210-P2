using UnityEngine;

public class Task : ScriptableObject
{
    public string taskName;
    public float timeToComplete = 10f;
    public ProficiencyType requiredProficiency;

    public virtual void OnSuccess() { }
}