using UnityEngine;

public class Storage : MonoBehaviour
{

    public int decay = 1;
    void OnEnable()
    {
        ResourceManager.Instance.decayAmount += decay;
    }

    void OnDisable()
    {
        {  ResourceManager.Instance.decayAmount -= decay; }
    }
}
