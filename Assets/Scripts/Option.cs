using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Option : MonoBehaviour
{
    public event Action<int, int> OnOptionChange;
    public int currentTime = 0;
    [SerializeField]private int maxTime = 1000;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>())
        {
            currentTime++;
            OnOptionChange?.Invoke(currentTime, maxTime);
            if (currentTime >= maxTime)
            {
                Debug.Log("Option Chosen.");
            }
        }
    }
}
