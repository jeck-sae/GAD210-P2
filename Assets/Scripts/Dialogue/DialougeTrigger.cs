using UnityEngine;

public class DialougeTrigger : MonoBehaviour
{
    public Dialouge[] dialouges;
    public DialougeCharacter[] characters;

    [System.Obsolete]
    private void Start()
    {
        FindObjectOfType<DialougeManager>().OpenDialouge(dialouges, characters);
    }
}

[System.Serializable]
public class Dialouge
{
    public int charId;

    [TextArea(3, 10)]
    public string dialouge;
}

[System.Serializable]
public class DialougeCharacter
{
    public string characterName;
    public Sprite charSprite;
}