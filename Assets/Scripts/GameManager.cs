using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [HideInEditorMode, ShowInInspector]
    List<Team> teams = new();

    private void Awake()
    {
        var characters = FindObjectsByType<Character>(FindObjectsSortMode.None);

        foreach (Character c in characters)
            teams.Add(new Team(new List<Character> { c }));

        var t = FindAnyObjectByType<Task>();

        TaskTripManager.Instance.StartTrip(teams[0], t);
    }


}
