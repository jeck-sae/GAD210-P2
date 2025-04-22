using System.Linq;
using UnityEngine;
[RequireComponent (typeof(Room))]
public class TaskGfx : MonoBehaviour
{
    
    [SerializeField] SpriteRenderer taskIconRenderer;
    Room room;

    TaskStatus status;

    int urgentResourceThreshold = 10;

    public Task Task => room.assignedTask;

    void Start()
    {
        room = GetComponent<Room>();
        taskIconRenderer.sprite = Task.icon;
        SetNormal();
    }

    private void Update()
    {
        if(Task is Disaster)
        {
            bool active = !DisasterManager.Instance.inactiveDisasters.Any(x => x.room.assignedTask == Task);
            if (status != TaskStatus.Urgent && active)
            {
                SetUrgent();
            }
            else if (status != TaskStatus.Unavailable && !active)
            {
                SetUnavailable();
            }
        }
        else if (Task is PassiveTask)
        {
            var resourceType = (Task as PassiveTask).income.resourceType;
            var amount = ResourceManager.Instance.GetResource(resourceType);

            if (status != TaskStatus.Urgent && amount <= urgentResourceThreshold)
                SetUrgent();
            else if (status != TaskStatus.Normal && amount > urgentResourceThreshold)
                SetNormal();
        }
    }

    void SetUrgent()
    {
        status = TaskStatus.Urgent;
        taskIconRenderer.color = new Color(1, 0, 0, taskIconRenderer.color.a);
    }

    void SetUnavailable()
    {
        status = TaskStatus.Unavailable;
        taskIconRenderer.color = new Color(.5f, .5f, .5f, taskIconRenderer.color.a);
    }

    void SetNormal()
    {
        status = TaskStatus.Normal;
        taskIconRenderer.color = new Color(1, 1, 1, taskIconRenderer.color.a);
    }

    enum TaskStatus { Normal, Unavailable, Urgent }
}
