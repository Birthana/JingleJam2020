﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerController>())
            collision.gameObject.GetComponent<PlayerController>().isJumping = false;
    }
}
