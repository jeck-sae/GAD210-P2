using UnityEngine;

public class InteractionManager : Singleton<InteractionManager>
{
    [SerializeField] LayerMask unitLayer;
    [SerializeField] LayerMask roomLayer;
    [SerializeField] Unit activeUnit;

    void Update()
    {
        if (!Input.GetMouseButtonDown(0) || Camera.main == null) return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D unitHit = Physics2D.GetRayIntersection(ray, unitLayer);

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
                return;
            }
        }

        if (activeUnit != null)
        {
            RaycastHit2D roomHit = Physics2D.GetRayIntersection(ray, roomLayer);

            if (roomHit.collider != null)
            {
                Room room = roomHit.collider.GetComponent<Room>();
                if (room != null && room.pathToRoom != null)
                {
                    activeUnit.StartPath(room.pathToRoom);
                }
            }
        }

        DeselectActiveUnit();
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