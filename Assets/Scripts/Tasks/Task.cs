using System;
using UnityEngine;

public class Task : ScriptableObject
{
    public string taskName;
    public float timeToComplete = 10f;
    public ProficiencyType requiredProficiency;
    public event Action OnSuccess;
    public void Success() 
    {
        DoSuccess();
        OnSuccess?.Invoke();
    }
    protected virtual void DoSuccess() { } 
}