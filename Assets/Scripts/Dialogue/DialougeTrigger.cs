using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    public Dialouge[] dialouges;
    public DialougeCharacter[] characters;
}

[System.Serializable]
public class Dialouge
{
    public int charId;
    public string dialouge;
}

[System.Serializable]
public class DialougeCharacter
{
    public int charName;
    public Sprite charSprite;
}