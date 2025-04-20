using UnityEngine;
using UnityEngine.SceneManagement;

public class EndScreen : Singleton<EndScreen>
{
    public GameObject LooseScreen;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
        if (LooseScreen.activeSelf) 
        { 
            LooseScreen.SetActive(false);  
        }
        else
        {
            Debug.Log("Aready OFF");
        }
    }
    public void GAMEOVER()
    {
        LooseScreen.SetActive(true);
        Time.timeScale = 0f;
    }

    public void restart()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
