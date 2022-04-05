using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarBehaviour : MonoBehaviour
{
    public static BarBehaviour Instance;
    public bool _isPlaying = false;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void OnTriggerEnter2D(Collider2D other)
    {
        _isPlaying = true;
    }
    public void OnTriggerExit2D(Collider2D other)
    {
        _isPlaying = false;
    }
}
