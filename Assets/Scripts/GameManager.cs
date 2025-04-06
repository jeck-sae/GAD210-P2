using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    List<Task> tasks;

    [SerializeField] 
    List<Character> characetrs;

    [HideInEditorMode, ShowInInspector]
    List<Team> teams;

}
