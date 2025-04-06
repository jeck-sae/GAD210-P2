using UnityEngine;
using UnityEngine.UI;

public class LevelElementColor : MonoBehaviour
{
    public enum LevelElement
    {
        Wall,
        Floor,
        Overlay,
        UI
    }

    [SerializeField] LevelElement objectType;

    private void Awake()
    {
        RegisterObject();
    }
    private void RegisterObject()
    {
        if (objectType == LevelElement.UI)
        {
            Image image = GetComponent<Image>();

            if (image)
            LevelColor.Instance.AddSprite("ui", null, image);

            return;
        }

        SpriteRenderer renderer = GetComponent<SpriteRenderer>();
        if (renderer != null)
        {
            if (objectType == LevelElement.Wall)
            LevelColor.Instance.AddSprite("wall", renderer);
            if (objectType == LevelElement.Floor)
            LevelColor.Instance.AddSprite("floor", renderer);
            if (objectType == LevelElement.Overlay)
            LevelColor.Instance.AddSprite("overlay", renderer);
        }
    }
}
