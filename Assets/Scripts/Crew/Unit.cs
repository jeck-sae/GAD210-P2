using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public CharacterUI progressBarUI;
    [SerializeField] GameObject overlay;
    [SerializeField] Character character;
    [HideInInspector] public bool IsMoving = false;

    private int progress;
    private bool returning = false;
    private PathToRoom currentPath;
    private Room currentRoom;
    private GameObject abortButton;

    private void Start()
    {
        abortButton = progressBarUI.AbortButton;
        abortButton.GetComponent<Button>().onClick.AddListener(StartPathBack);
        abortButton.SetActive(false);

        Room hubRoom = RoomManager.GetRoom(RoomID.Hub);
        currentRoom = hubRoom;
        hubRoom.AddCharacter(this);
        UpdateLabel(hubRoom.name);

        progressBarUI.SetName(character.characterName);
    }
    public void StartPath(PathToRoom path)
    {
        Debug.Log("Unit starts path");
        IsMoving = true;
        returning = false;
        currentPath = path;
        progress = 0;

        currentRoom?.RemoveCharacter(this); // Remove from previous room
        UpdateLabel("");
        MoveToNextStop();
    }

    public void StartPathBack()
    {
        Debug.Log("Unit starts path back");
        StopAllCoroutines();
        UpdateProgress(0);

        if (abortButton)
        abortButton.SetActive(false);
        UpdateLabel("");

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

            currentRoom?.RemoveCharacter(this); // Remove from previous room
            currentRoom = finalRoom;
            currentRoom.AddCharacter(this);

            if (finalRoom.assignedTask)
            {
                StartTask(finalRoom);
            }
            else
            {
                StartPathBack();
            }

            return;
        }

        if (beforeHub)
        {
            Debug.Log("Returned to hub");

            currentRoom?.RemoveCharacter(this); // Remove from previous room
            currentRoom = RoomManager.GetRoom(RoomID.Hub);
            currentRoom.AddCharacter(this);

            IsMoving = false;
            currentPath = null;
            return;
        }

        Way nextWay = currentPath.WayToRoom[progress];
        Room nextRoom = RoomManager.GetRoom(nextWay.NextRoom);

        currentRoom?.RemoveCharacter(this); // Remove from current room before entering the next
        currentRoom = nextRoom;
        currentRoom.AddCharacter(this);

        StartCoroutine(WaitAndMove(nextWay.TimeToWait));
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
        if (abortButton)
        abortButton.SetActive(true);

        UpdateLabel(room.name);
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

            UpdateProgress(progress);

            yield return null;
        }

        room.assignedTask.OnSuccess();
        UpdateProgress(0);

        if (room.assignedTask is PassiveTask)
        {
            StartTask(room);
        }
        else
        {
            StartPathBack();
        }
    }
    public void UpdateLabel(string label)
    {
        if (progressBarUI)
        {
            progressBarUI.SetLabel(label);
        }
    }
    public void UpdateProgress(float progress)
    {
        if (progressBarUI)
        {
            progressBarUI.UpdateProgress(progress);
        }
    }

    public void SetUnitActive()
    {
        overlay.SetActive(true);
        if (progressBarUI)
        progressBarUI.SetUnitActive();
    }

    public void SetUnitInActive()
    {
        overlay.SetActive(false);
        if (progressBarUI)
        progressBarUI.SetUnitInActive();
    }
}