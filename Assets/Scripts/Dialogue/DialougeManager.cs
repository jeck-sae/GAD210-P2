using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;

public class DialougeManager : MonoBehaviour
{
    public Image charImage;
    public TMP_Text charName;
    public TMP_Text dialougeText;
    public RectTransform backgroundBox;

    Dialouge[] currentDialouge;
    DialougeCharacter[] currentDialougeCharacter;
    int activeDialouge = 0;
    public static bool  isActive = false;
   [SerializeField] GameObject dialouge; 

    public void OpenDialouge(Dialouge[] dialouges, DialougeCharacter[] characters)
    {
        currentDialouge = dialouges;
        currentDialougeCharacter = characters;
        activeDialouge = 0;
        isActive = true;

        Debug.Log("Started dialouge. Loaded dialouge: " + dialouges.Length);
        DisplayDialouge();
    }

    void DisplayDialouge()
    {
        Dialouge dialougeToDisplay = currentDialouge[activeDialouge];
        dialougeText.text = dialougeToDisplay.dialouge;

        DialougeCharacter charToDisplay = currentDialougeCharacter[dialougeToDisplay.charId];
        charName.text = charToDisplay.characterName;
        charImage.sprite = charToDisplay.charSprite;

    }

    public void NextDialouge()
    {
        activeDialouge++;
        if (activeDialouge < currentDialouge.Length)
        {
            DisplayDialouge();
        }
        else
        {
            Debug.Log("Dialouge Ended");
            isActive = false;
            dialouge.SetActive(false);
            SceneManager.LoadScene("TheGame");
        }
       
    }

    public void SkipDialogue()
    {
        SceneManager.LoadScene("TheGame");
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isActive == true)
        {
            NextDialouge();
        }

        if (Input.GetMouseButtonDown(0) && isActive == true)
        {
            NextDialouge();
        }
    }
}

