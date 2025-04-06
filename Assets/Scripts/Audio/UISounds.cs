using UnityEngine;
using UnityEngine.UI;

public class UISounds : MonoBehaviour
{
    private void Awake()
    {
        if (TryGetComponent(out Button btn))
        btn.onClick.AddListener(() => Play(AudioManager.Instance.buttonSound));
    }

    private void Play(AudioClip clip, float volume = 1f)
    {
        AudioManager.Instance.PlaySound(clip, volume);
    }
}
