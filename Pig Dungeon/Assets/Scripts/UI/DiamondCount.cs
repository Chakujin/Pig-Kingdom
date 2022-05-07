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
        int secondNum = 0;
        m_gameManager.playerDiamond = currentCount;

        while (currentCount >= 10)
        {
            currentCount -= 10;
            secondNum++;
        }

        switch (currentCount)
        {
            case 0:
                myRender2.sprite = numbers[0];
                break;

            case 1:
                myRender2.sprite = numbers[1];
                break;

            case 2:
                myRender2.sprite = numbers[2];
                break;

            case 3:
                myRender2.sprite = numbers[3];
                break;

            case 4:
                myRender2.sprite = numbers[4];
                break;

            case 5:
                myRender2.sprite = numbers[5];
                break;

            case 6:
                myRender2.sprite = numbers[6];
                break;

            case 7:
                myRender2.sprite = numbers[7];
                break;

            case 8:
                myRender2.sprite = numbers[8];
                break;

            case 9:
                myRender2.sprite = numbers[9];
                break;
        }

        switch (secondNum)
        {
            case 0:
                myRender.sprite = numbers[0];
                break;

            case 1:
                myRender.sprite = numbers[1];
                break;

            case 2:
                myRender.sprite = numbers[2];
                break;

            case 3:
                myRender.sprite = numbers[3];
                break;

            case 4:
                myRender.sprite = numbers[4];
                break;

            case 5:
                myRender.sprite = numbers[5];
                break;

            case 6:
                myRender.sprite = numbers[6];
                break;

            case 7:
                myRender.sprite = numbers[7];
                break;

            case 8:
                myRender.sprite = numbers[8];
                break;

            case 9:
                myRender.sprite = numbers[9];
                break;
        }
    }
}
