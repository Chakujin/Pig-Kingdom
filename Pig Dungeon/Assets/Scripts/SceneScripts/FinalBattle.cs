﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalBattle : MonoBehaviour
{
    public EnemyKing enemyKing;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            Debug.Log("start");
            enemyKing.startFigth = true;
        }
    }
}