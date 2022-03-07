using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private GameManager m_gameManager;

    private void Awake()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
    }

    public void Contiune()
    {
        m_gameManager.loadGame();

        SceneManager.LoadScene(m_gameManager.level);
    }

    public void NewGame()
    {
        m_gameManager.saveGame();
        m_gameManager.level = 1;
        SceneManager.LoadScene(1);
    }
}
