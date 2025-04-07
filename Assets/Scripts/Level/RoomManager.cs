using System.Collections.Generic;
using UnityEngine;
public enum RoomID
{
    Hub,
    Communications,
    Bridge,
    MedicalBay,
    Kitchen,
    Storage,
    Airlock,
    CorridorCenter,
    CorridorBottom,
    CorridorLeft,
    CorridorRight,
    CorridorTop
}

public class RoomManager : Singleton<RoomManager>
{

    private static Dictionary<RoomID, Room> rooms = new();

    public static void Register(Room room)
    {
        if (!rooms.ContainsKey(room.roomID))
        rooms.Add(room.roomID, room);
    }

    public static void Unregister(Room room)
    {
        if (rooms.ContainsKey(room.roomID))
        rooms.Remove(room.roomID);
    }

    public static Room GetRoom(RoomID id)
    {
        rooms.TryGetValue(id, out var room);
        if (room)
        {
            return room;
        }
        else
        {
            Debug.LogWarning("Room not found!");
            return null;
        }
    }
}