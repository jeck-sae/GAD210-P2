using System.Collections.Generic;
using UnityEngine;

public enum Stat { Stat1, Stat2, Stat3 };

public interface IStats
{
    public float GetStat(Stat stat);
}