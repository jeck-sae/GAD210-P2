using System.Collections;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class MainMenu : MonoBehaviour
{
    [SerializeField] GameObject mainMenuUI;
    [SerializeField] GameObject mainBlock;
    [SerializeField] GameObject settings;
    [SerializeField] GameObject gameUI;
    [SerializeField] GameObject level;
    [SerializeField] Volume globalVolume;
    [SerializeField] AnimationCurve curveIn;
    [SerializeField] AnimationCurve curveOut;
    [SerializeField] float phase1Duration = 2f;
    [SerializeField] float phase2Duration = 1f;
    [SerializeField] float easeOutAmount = 0.85f;
    private LensDistortion lens;

    void Start()
    {
        if (globalVolume.profile.TryGet(out lens))
        {
            lens.scale.value = 1f;
        }
        else
        {
            Debug.LogWarning("lens");
        }
        level.SetActive(false);
        gameUI.SetActive(false);
        settings.SetActive(false);
        mainBlock.SetActive(true);
        mainMenuUI.SetActive(true);
    }
    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        StartCoroutine(StartingGame());
    }
    public void OpenMainMenu()
    {

    }
    public void Restart()
    {

    }
    private IEnumerator StartingGame()
    {
        level.SetActive(true);

        // Phase 1
        float timeElapsed = 0f;
        while (timeElapsed < phase1Duration)
        {
            float t = timeElapsed / phase1Duration;
            lens.scale.value = curveIn.Evaluate(t);
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        lens.scale.value = curveIn.Evaluate(1f);

        // Phase 2
        timeElapsed = 0f;
        float startValue = lens.scale.value;
        float endValue = easeOutAmount;
        while (timeElapsed < phase2Duration / 2f)
        {
            float t = timeElapsed / (phase2Duration / 2f);
            lens.scale.value = Mathf.Lerp(startValue, endValue, curveOut.Evaluate(t));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Phase 3
        timeElapsed = 0f;
        startValue = lens.scale.value;
        endValue = 1f;
        while (timeElapsed < phase2Duration / 2f)
        {
            float t = timeElapsed / (phase2Duration / 2f);
            lens.scale.value = Mathf.Lerp(startValue, endValue, curveOut.Evaluate(t));
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        lens.scale.value = 1f;
        SetGamesUI();
    }
    private void SetGamesUI()
    {
        gameUI.SetActive(true);
    }
    public void QuitGame()
    {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }
}