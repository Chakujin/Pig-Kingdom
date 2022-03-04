﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondCount : MonoBehaviour
{

    private PlayerMovement m_playermovement;
    public Sprite[] numbers;
    public Image myRender;
    public Image myRender2;

    // Start is called before the first frame update
    void Start()
    {
        m_playermovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        updateDiamondCount(m_playermovement.diamondTotal);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateDiamondCount(int currentCount)
    {
        int secondNum;
        if (currentCount <= 9)
        {
            secondNum = currentCount - 1;

            myRender.sprite = numbers[9]; // 9 = 0
            myRender2.sprite = numbers[secondNum];
        }
        if (currentCount <= 19 && currentCount >= 10)
        {
            secondNum = currentCount - 11;
            if (secondNum == -1) // only pass if secon num is 0
            {
                secondNum = 9;
            }

            myRender.sprite = numbers[0]; // 0 = 1
            myRender2.sprite = numbers[secondNum];
        }
    }
}