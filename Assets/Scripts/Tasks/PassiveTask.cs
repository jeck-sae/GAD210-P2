using UnityEngine;

public enum ResourceType
{
    Oxygen,
    Energy,
    Food
}

[CreateAssetMenu(fileName = "PassiveTask", menuName = "Scriptable Objects/PassiveTask")]
public class PassiveTask : Task
{
    [Header("On Complition")]
    public ResourceIncome income;

    protected override void DoSuccess()
    {
        ResourceManager.Instance.AddResource(income.resourceType, income.modifier);
    }
}

[System.Serializable]
public class ResourceIncome
{
    public ResourceType resourceType;
    public int modifier;
}