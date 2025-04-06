using Unity.VisualScripting;
using UnityEngine;

public abstract class Task : MonoBehaviour
{
    public Room room;
    public float taskDuration;
    public abstract bool CanAttempt();
    public abstract bool Attempt(Team team);

}

