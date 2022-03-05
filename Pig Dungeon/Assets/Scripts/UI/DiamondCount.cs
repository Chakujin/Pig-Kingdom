using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DiamondCount : MonoBehaviour
{

    private PlayerMovement m_playermovement;
    public Sprite[] numbers;
    public Image myRender;
    public Image myRender2;

    private GameManager m_gameManager;

    // Start is called before the first frame update
    void Start()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        m_playermovement = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();


        updateDiamondCount(m_gameManager.playerDiamond);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //Update inGame
    public void updateDiamondCount(int currentCount)
    {
        m_gameManager.playerDiamond = currentCount;

        int secondNum;
        if (currentCount <= 9)
        {
            secondNum = currentCount - 1;
            if (secondNum == -1) // only pass if secon num is 0
            {
                secondNum = 9;
            }

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
