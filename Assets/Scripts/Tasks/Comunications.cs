using UnityEngine;
using System.Collections;

public class Comunications : MonoBehaviour 
{
    public GameObject noEnergy;
    public GameObject noFood;
    public GameObject noOxegen;
    public GameObject Energy;
    public GameObject Food;
    public GameObject Oxegen;
    private float delayTime = 1f;

    void OnEnable()
    {
        StartCoroutine(Sabotage());
    }
    private void OnDisable()
    {
        Energy.SetActive(true);
        Food.SetActive(true);
        Oxegen.SetActive(true);
        noEnergy.SetActive(false);
        noFood.SetActive(false);
        noOxegen.SetActive(false);
    }

    IEnumerator Sabotage()
    {
        yield return new WaitForSeconds(delayTime);
        Energy.SetActive(false);
        noEnergy.SetActive(true);
        yield return new WaitForSeconds(delayTime);
        noFood.SetActive(true);
        Food.SetActive(false);
        yield return new WaitForSeconds(delayTime);
        noOxegen.SetActive(true);
        Oxegen.SetActive(false); 
    }

}
