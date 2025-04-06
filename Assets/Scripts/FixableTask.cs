using UnityEngine;

public class FixableTask : Task
{
    bool isFixed;
    public Stat requiredStat;

    public override bool Attempt(Team team)
    {
        Debug.Log("Task");
        if (Random.Range(0, 100) >= team.GetStat(requiredStat))
        {
            return false;
        }

        isFixed = true;

        return true;
    }

    public override bool CanAttempt()
    {
        return !isFixed;
    }


}