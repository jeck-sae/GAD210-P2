using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelColor : Singleton<LevelColor>
{
    [SerializeField] Color wallColor = Color.green;
    [SerializeField] Color UIColor = Color.green;
    [SerializeField] Color floorColor= Color.grey;
    [SerializeField] Color overlayColor = Color.white;

    private List<SpriteRenderer> walls = new List<SpriteRenderer>();
    private List<SpriteRenderer> floors = new List<SpriteRenderer>();
    private List<SpriteRenderer> overlays = new List<SpriteRenderer>();
    private List<Image> ui = new List<Image>();

    public void AddSprite(string obj, SpriteRenderer renderer = null, Image image = null)
    {
        switch (obj)
        {
            case "wall":
                walls.Add(renderer);
                break;
            case "floor":
                floors.Add(renderer);
                break;
            case "overlay":
                overlays.Add(renderer);
                break;
            case "ui":
                ui.Add(image);
                break;
            default:
                Debug.LogWarning("Unknown object type: " + obj);
                break;
        }
    }
    private void Start()
    {
        foreach (var sprite in walls)
        {
            sprite.color = wallColor;
        }
        foreach (var sprite in floors)
        {
            sprite.color = floorColor;
        }
        foreach (var sprite in overlays)
        {
            sprite.color = overlayColor;
        }
        foreach (var image in ui)
        {
            image.color = UIColor;
        }
    }
}