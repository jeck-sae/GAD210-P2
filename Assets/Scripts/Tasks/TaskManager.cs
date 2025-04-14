using System.Collections.Generic;
using UnityEngine;

public enum ProficiencyType
{
    Engineering,
    Hacking,
    Medical,
    Chemistry,
}
public class TaskManager : Singleton<TaskManager>
{

    public float GetTaskTime(Character character, Task task)
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

        float multiplier = level switch
        {
            1 => 0.7f,  // 30% faster
            2 => 0.5f,  // 50% faster
            3 => 0.3f,  // 70% faster
            _ => 1.0f   // No proficiency, full time
        };

        return task.timeToComplete * multiplier;
    }

    public void UpdateProgress(Unit unit, float progress)
    {
        if (unit.progressBarUI)
        {
            unit.progressBarUI.UpdateProgress(progress);
        }
    }
}