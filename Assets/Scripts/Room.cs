using UnityEngine;

public class Room : MonoBehaviour
{
    public PathToRoom pathToRoom;
    public RoomID roomID;
    public float timeToCross;
    public Room nextRoom; //next room to get to the hub

    void Awake()
    {
        RoomManager.Register(this);
    }

    void OnDestroy()
    {
        RoomManager.Unregister(this);
    }
    public void RemoveTeam(Team team)
    {
        //hide characters
    }
    public void AddTeam(Team team)
    {
        Debug.Log(team.characters.Count + " moved to " + name, gameObject);
        //show characters
    }
}
