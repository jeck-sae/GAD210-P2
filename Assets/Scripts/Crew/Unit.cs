using UnityEngine;
using System.Collections;

public class Unit : MonoBehaviour
{
    [SerializeField] GameObject overlay;
    [HideInInspector] public bool IsMoving = false;

    private int progress;
    private int stops;
    private bool returning = false;
    private PathToRoom currentPath;

    public void StartPath(PathToRoom path)
    {
        Debug.Log("Unit starts path");
        IsMoving = true;
        currentPath = path;
        progress = 0;
        stops = path.WayToRoom.Length;

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
        bool endOfForwardPath = !returning && progress >= stops;
        bool endOfReturnPath = returning && progress < 0;

        if (endOfForwardPath || endOfReturnPath)
        {
            if (endOfForwardPath)
            {
                Debug.Log("Reached final room");
                IsMoving = false;
                StartPathBack();
                return;
            }

            Debug.Log("Returned to hub");
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
    public void SetUnitActive()
    {
        overlay.SetActive(true);
    }
    public void SetUnitInActive()
    {
        overlay.SetActive(false);
    }
}