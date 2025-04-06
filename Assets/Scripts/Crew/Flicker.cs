using UnityEngine;
using System.Collections;
using static Unity.Collections.AllocatorManager;

public class Flicker : MonoBehaviour
{
    [SerializeField] SpriteRenderer _renderer;
    [SerializeField] float flickerInterval = 0.1f;
    private float timer = 0f;
    private bool flick = true;

    private void Start()
    {
        if (_renderer == null)
        _renderer = GetComponent<SpriteRenderer>();
    }
    private void Update()
    {
        _renderer.enabled = flick;
        timer += Time.deltaTime;

        if (timer > flickerInterval)
        {
            flick = !flick;
            timer = 0f;
        }
    }
}