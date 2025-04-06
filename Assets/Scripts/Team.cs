using System.Collections.Generic;
using UnityEngine;

public class Team : IStats
{


    public Team(List<Character> characters)
    {
        this.characters = characters;
    }

    public List<Character> characters;

    public List<Team> Split()
    {
        List<Team> newTeams = new();
        foreach (Character c in characters)
            newTeams.Add(new Team(new List<Character>() { c }));

        return newTeams;
    }

    public void Merge(Team mergeWith)
    {
        characters.AddRange(mergeWith.characters);
    }


    //calculates the stats as if every character attempted
    //to complete the task one after the other e.g. (50%, 40%) => 70%
    public float GetStat(Stat stat)
    {
        float value = 0;
        
        foreach (Character c in characters)
            value *= 1 - c.GetStat(stat) / 100;
        
        return (1 - value) * 100;
    }
}
