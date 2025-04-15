using UnityEngine;

public class InteractionManager : Singleton<InteractionManager>
{
    [SerializeField] LayerMask unitLayer;
    [SerializeField] LayerMask roomLayer;
    private Unit activeUnit;
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (activeUnit == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, unitLayer);

                if (hit.collider != null)
                {
                    Unit unit = hit.collider.GetComponent<Unit>();
                    if (unit == activeUnit)
                    {
                        return;
                    }
                    else if (unit)
                    {
                        if (unit.IsMoving)
                        {
                            DeselectActiveUnit();
                            return;
                        }
                        SetActiveUnit(unit);
                    }
                    else
                    {
                        DeselectActiveUnit();
                    }
                }
            }
            else
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit2D hit = Physics2D.GetRayIntersection(ray, roomLayer);

                if (hit.collider != null)
                {
                    Room room = hit.collider.GetComponent<Room>();
                    if (room)
                    {
                        if (room.pathToRoom != null)
                        {
                            activeUnit.StartPath(room.pathToRoom);
                            DeselectActiveUnit();
                        }
                    }
                }
            }
        }
        if (Input.GetMouseButtonDown(1))
        {
            if (activeUnit == null) return;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, roomLayer);

            if (hit.collider != null)
            {
                Room room = hit.collider.GetComponent<Room>();
                if (room)
                {
                    if (room.pathToRoom != null)
                    {
                        activeUnit.StartPath(room.pathToRoom);
                        DeselectActiveUnit();
                    }
                }
            }
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