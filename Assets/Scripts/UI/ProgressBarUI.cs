using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private Image fillImage;
    [SerializeField] private Text labelText;

    public void SetLabel(string label)
    {
        if (labelText != null)
        labelText.text = label;
    }

    public void UpdateProgress(float progress)
    {
        fillImage.fillAmount = Mathf.Clamp01(progress);
    }
}