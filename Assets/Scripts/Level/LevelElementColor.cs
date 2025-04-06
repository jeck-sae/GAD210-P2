using UnityEngine;

public class LevelElementColor : MonoBehaviour
{
    public enum LevelElement
    {
        Wall,
        Floor,
        Overlay
    }

    [SerializeField] LevelElement objectType;

    private void Awake()
    {
        RegisterObject();
    }
    private void RegisterObject()
    {
        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            if (objectType == LevelElement.Wall)
            LevelColor.Instance.AddSprite(renderer, "wall");
            if (objectType == LevelElement.Floor)
            LevelColor.Instance.AddSprite(renderer, "floor");
            if (objectType == LevelElement.Overlay)
            LevelColor.Instance.AddSprite(renderer, "overlay");
        }
    }
}
