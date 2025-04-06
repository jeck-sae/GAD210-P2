using UnityEngine;

[CreateAssetMenu(fileName = "PathToRoom", menuName = "Scriptable Objects/Path To Room")]
public class PathToRoom : ScriptableObject
{
    public Way[] WayToRoom;
}

[System.Serializable]
public class Way
{
    public RoomID NextRoom;
    public float TimeToWait;
}