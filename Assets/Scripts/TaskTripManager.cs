using System.Collections;
using System.Collections.Generic;
using TowerDefense;
using UnityEngine;

public class TaskTripManager : Singleton<TaskTripManager>
{
    public void StartTrip(Team team, Task task)
    {
        StartCoroutine(Trip(team, task));
    }

    protected IEnumerator Trip(Team team, Task task)
    {
        List<Room> path = new();
        Room current = task.room;
        while(current != null)
        {
            path.Insert(0, current);
            current = current.nextRoom;
        }

        //move towards task
        for (int i = 1; i < path.Count; i++) 
        {
            team.ChangeRoom(path[i]);
            yield return Helpers.GetWait(path[i].timeToCross);
        }

        //attempt task
        yield return Helpers.GetWait(task.taskDuration);
        task.Attempt(team);

        //go back to hub
        for (int i = path.Count - 2; i >= 0; i--) 
        {
            team.ChangeRoom(path[i]);
            yield return Helpers.GetWait(path[i].timeToCross);
        }
    }
}
