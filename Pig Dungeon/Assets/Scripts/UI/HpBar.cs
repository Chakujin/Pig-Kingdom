﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HpBar : MonoBehaviour
{
    public GameObject[] hearts;
    private PlayerMovement m_playermovement;
    private int i_currentHeal; 

    // Start is called before the first frame update
    void Start()
    {
        m_playermovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        i_currentHeal = m_playermovement.currentHealth;

        for (int i = 0; i<m_playermovement.currentHealth; i++)
        {
            hearts[i].SetActive(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void addHeart()
    {
        i_currentHeal = m_playermovement.currentHealth;
        switch (i_currentHeal)
        {
            case 0:
                break;
            case 1:
                hearts[1].SetActive(true);
                break;
            case 2:
                hearts[2].SetActive(true);
                break;
            case 3:
                hearts[3].SetActive(true);
                break;
            default:
                Debug.Log("Peta");
                break;
        }
    }
}