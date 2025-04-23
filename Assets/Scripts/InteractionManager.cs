using UnityEngine;

public class InteractionManager : Singleton<InteractionManager>
{
    [SerializeField] LayerMask unitLayer;
    [SerializeField] LayerMask roomLayer;
    [SerializeField] AudioClip[] interactionClip;
    [SerializeField] Unit activeUnit;

    private Room lastHoveredRoom = null;

    void Update()
    {
        HoverOverRoom();

        if (!Input.GetMouseButtonDown(0) || Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D unitHit = Physics2D.GetRayIntersection(ray, 100, unitLayer);

        if (unitHit.collider != null)
        {
            Unit clickedUnit = unitHit.collider.GetComponent<Unit>();

            if (clickedUnit != null)
            {
                if (clickedUnit.IsMoving)
                {
                    DeselectActiveUnit();
                    return;
                }

                SetActiveUnit(clickedUnit);
                AudioManager.Instance.PlayRandomSound(interactionClip, 0.5f);
                return;
            }
        }

        if (activeUnit != null)
        {
            RaycastHit2D roomHit = Physics2D.GetRayIntersection(ray, 100, roomLayer);

            if (roomHit.collider != null)
            {
                Room room = roomHit.collider.GetComponent<Room>();
                if (room != null && room.pathToRoom != null)
                {
                    activeUnit.StartPath(room.pathToRoom);
                    AudioManager.Instance.PlayRandomSound(interactionClip, 0.5f);
                }
            }
        }

        DeselectActiveUnit();
    }
    private void HoverOverRoom()
    {
        if (Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D roomHit = Physics2D.GetRayIntersection(ray, 100, roomLayer);


        if (roomHit.collider != null)
        {
            Room hoveredRoom = roomHit.collider.GetComponent<Room>();

            if (hoveredRoom != null && hoveredRoom.pathToRoom != null)
            {
                if (hoveredRoom != lastHoveredRoom)
                {
                    if (lastHoveredRoom != null)
                    {
                        lastHoveredRoom.OnHoverExit();
                    }
                    hoveredRoom.OnHoverEnter();
                    lastHoveredRoom = hoveredRoom;
                }
                else 
                {
                    hoveredRoom.OnHoverEnter();
                    lastHoveredRoom = hoveredRoom;
                }
            }
        }
        else if (lastHoveredRoom != null)
        {
            lastHoveredRoom.OnHoverExit();
        }
    }
    private void SetActiveUnit(Unit unit)
    {
        DeselectActiveUnit();
        activeUnit = unit;
        activeUnit.SetUnitActive();
    }
    private void DeselectActiveUnit()
    {
        if (activeUnit == null) return;

        activeUnit.SetUnitInActive();
        activeUnit = null;
    }
}