using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;


//using OdinInspector's SerializedMonoBehaviour to save changes made to
//the stats dictionary from the inspector (Unity doesn't save dictionaries by default)
public class Character : SerializedMonoBehaviour, IStats
{
    [ShowInInspector]
    protected Dictionary<Stat, float> stats;

    public float GetStat(Stat stat)
    {
        if(stats.TryGetValue(stat, out float value))
            return value;

        Debug.LogWarning($"Missing stat: {stat.ToString()} ({name})", gameObject);
        return 0;
    }




    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
