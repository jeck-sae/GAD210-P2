using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public float timeToCross;
    public Room nextRoom; //next room to get to the hub

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
