using UnityEngine;

public class Room : MonoBehaviour
{
    public PathToRoom pathToRoom;
    public RoomID roomID;
    public Task assignedTask;

    void Awake()
    {
        RoomManager.Register(this);
    }

    void OnDestroy()
    {
        RoomManager.Unregister(this);
    }
    public void RemoveCharacter()
    {

    }
    public void AddCharacter()
    {

    }
}