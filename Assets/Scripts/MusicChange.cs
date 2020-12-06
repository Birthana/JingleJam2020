using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicChange : MonoBehaviour
{
    private bool isPlayed = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerController>() && !isPlayed)
        {
            isPlayed = true;
            AudioManager.instance.PlayBackGround(10);
        }
    }
}
