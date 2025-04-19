using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CharacterUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private TextMeshProUGUI labelText;
    [SerializeField] private TextMeshProUGUI nameText;
    public GameObject AbortButton;
    public Image portretOverlay;

    private void Start()
    {
        if (portretOverlay)
        portretOverlay.gameObject.SetActive(false);
    }
    public void SetName(string name)
    {
        if (nameText != null)
        nameText.text = name;
    }
    public void SetLabel(string label)
    {
        if (labelText != null)
        labelText.text = label;
    }
    public void UpdateProgress(float progress)
    {
        fillImage.fillAmount = Mathf.Clamp01(progress);
    }
    public void SetUnitActive()
    {
        if (portretOverlay)
        portretOverlay.gameObject.SetActive(true);
    }
    public void SetUnitInActive()
    {
        if (portretOverlay)
        portretOverlay.gameObject.SetActive(false);
    }
}