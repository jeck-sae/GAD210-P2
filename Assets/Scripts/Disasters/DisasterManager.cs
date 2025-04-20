using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class DisasterManager : MonoBehaviour
{
    float startDelay = 30;
    float intervalDelay = 20;
    float intervalRandomRange = 5;

    public List<Disaster> disasters; 

    List<Disaster> inactiveDisasters = new();

    private void Start()
    {
        foreach(Disaster disaster in disasters)
        {
            inactiveDisasters.Add(disaster);
            disaster.room.assignedTask.OnSuccess += 
                () => { 
                    disaster.disasterBehaviour.enabled = false;
                    inactiveDisasters.Add(disaster);
                };
        }
        StartCoroutine(HandleDisasters());
    }

    IEnumerator HandleDisasters()
    {
        yield return new WaitForSeconds(startDelay);
        while (true)
        {
            float delay = intervalDelay + UnityEngine.Random.Range(-intervalRandomRange / 2, intervalRandomRange);
            yield return new WaitForSeconds(delay);
            if (inactiveDisasters.Count <= 0)
                continue;

            var disaster = inactiveDisasters[UnityEngine.Random.Range(0, inactiveDisasters.Count)];
            inactiveDisasters.Remove(disaster);
            disaster.disasterBehaviour.enabled = true;
        }
    }

    [Serializable]
    public class Disaster
    {
        public Room room;
        public MonoBehaviour disasterBehaviour;
    }
}