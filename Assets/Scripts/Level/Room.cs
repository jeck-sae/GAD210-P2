using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public PathToRoom pathToRoom;
    public RoomID roomID;
    public Task assignedTask;
    public Transform[] characterPos; // Size 5
    public List<Unit> unitList = new List<Unit>(5);

    void Awake()
    {
        RoomManager.Register(this);
    }

    void OnDestroy()
    {
        RoomManager.Unregister(this);
    }
    public void RemoveCharacter(Unit unit)
    {
        unitList.Remove(unit);
        UpdateCharacterPositions();
    }
    public void AddCharacter(Unit unit)
    {
        if (unitList.Contains(unit)) return;

        unitList.Add(unit);
        UpdateCharacterPositions();
    }
    private void UpdateCharacterPositions()
    {
        for (int i = 0; i < unitList.Count; i++)
        {
            if (i < characterPos.Length)
            {
                unitList[i].transform.position = characterPos[i].position;
            }
        }
    }
}