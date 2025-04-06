using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    [SerializeField] GameObject overlay;
    [HideInInspector] public bool IsMoving = false;
    private int progress;
    private int stops;

    public void StartPath(PathToRoom path)
    {
        Debug.Log("Unit starts path");
        IsMoving = true;
        progress = 0;
        stops = path.WayToRoom.Length;

        MoveToNextStop(path);
    }

    void MoveToNextStop(PathToRoom path)
    {
        if (progress >= stops)
        {
            IsMoving = false;
            Debug.Log("Final room");
            return;
        }

        Way currentWay = path.WayToRoom[progress];
        Room room = RoomManager.GetRoom(currentWay.NextRoom);

        transform.position = room.transform.position;

        StartCoroutine(WaitAndMove(currentWay.TimeToWait, room, path));
    }

    IEnumerator WaitAndMove(float waitTime, Room room, PathToRoom path)
    {
        yield return new WaitForSeconds(waitTime);

        transform.position = room.transform.position;

        progress++;
        MoveToNextStop(path);
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