using UnityEngine;
using UnityEngine.TextCore.Text;

public enum ProficiencyType
{
    Engineering,
    Hacking,
    Medical,
    Chemistry,
}
public class TaskManager : Singleton<TaskManager>
{
    public void PerformTask(Character character, Task task)
    {
        bool success = Roll(character, task);
        if (success)
        {
            task.OnSuccess();
            Debug.Log("Success!");
            return;
        }

        Debug.Log("Fail!");
    }
    private bool Roll(Character character, Task task)
    {
        float chance = GetSuccessChance(character, task);
        float roll = Random.Range(0f, 1f);
        return roll <= chance;
    }

    public float GetSuccessChance(Character character, Task task)
    {
        int level = 0;

        foreach (var prof in character.proficiencies)
        {
            if (prof.type == task.requiredProficiency)
            {
                level = prof.level;
                break;
            }
        }
        switch (level)
        {
            case 1:
                return 0.2f;
            case 2:
                return 0.5f;
            case 3:
                return 0.8f;
            default:
                return 0.1f; // if stats are diffrent
        }
    }
}