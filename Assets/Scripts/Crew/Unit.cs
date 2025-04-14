using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    public ProgressBarUI progressBarUI;
    [SerializeField] GameObject overlay;
    [SerializeField] Character character;
    [HideInInspector] public bool IsMoving = false;

    private int progress;
    private bool returning = false;
    private PathToRoom currentPath;

    public void StartPath(PathToRoom path)
    {
        Debug.Log("Unit starts path");
        IsMoving = true;
        returning = false;
        currentPath = path;
        progress = 0;

        MoveToNextStop();
    }

    public void StartPathBack()
    {
        Debug.Log("Unit starts path back");
        IsMoving = true;
        returning = true;
        progress = currentPath.WayToRoom.Length - 1;

        MoveToNextStop();
    }

    private void MoveToNextStop()
    {
        bool endOfForwardPath = !returning && progress >= currentPath.WayToRoom.Length;
        bool beforeHub = returning && progress < 0;

        if (endOfForwardPath)
        {
            Debug.Log("Reached final room");
            Room finalRoom = RoomManager.GetRoom(currentPath.WayToRoom[currentPath.WayToRoom.Length - 1].NextRoom);

            if (finalRoom.assignedTask)
            {
                StartTask(finalRoom);
                return;
            }
            else
            {
                StartPathBack();
                return;
            }
        }

        if (beforeHub)
        {
            Debug.Log("Returned to hub");

            Room hubRoom = RoomManager.GetRoom(RoomID.Hub);
            transform.position = hubRoom.transform.position;

            IsMoving = false;
            currentPath = null;
            return;
        }

        Way currentWay = currentPath.WayToRoom[progress];
        Room room = RoomManager.GetRoom(currentWay.NextRoom);

        transform.position = room.transform.position;

        StartCoroutine(WaitAndMove(currentWay.TimeToWait));
    }

    IEnumerator WaitAndMove(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);

        if (returning)
        progress -= 1;
        else
        progress += 1;

        MoveToNextStop();
    }

    private void StartTask(Room room)
    {
        float adjustedTime = TaskManager.Instance.GetTaskTime(character, room.assignedTask);
        StartCoroutine(PerformTaskRoutine(room, adjustedTime));
    }
    IEnumerator PerformTaskRoutine(Room room, float duration)
    {
        float timer = 0f;

        while (timer < duration)
        {
            timer += Time.deltaTime;
            float progress = timer / duration;

            TaskManager.Instance.UpdateProgress(this, progress);

            yield return null;
        }

        room.assignedTask.OnSuccess();
        TaskManager.Instance.UpdateProgress(this, 0);

        if (room.assignedTask is PassiveTask)
        {
            StartTask(room);
        }
        else
        {
            StartPathBack();
        }
    }

    public void SetUnitActive()
    {
        overlay.SetActive(true);
    }

    public void SetUnitInActive()
    {
        overlay.SetActive(false);
    }
}