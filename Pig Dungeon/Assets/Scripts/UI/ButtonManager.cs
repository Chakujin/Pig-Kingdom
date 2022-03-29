using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    private GameManager m_gameManager;
    public GameObject OptionMenu;
    public GameObject[] MainButtons;

    private void Awake()
    {
        m_gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();
        OptionMenu.SetActive(false);
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

    public void Options()
    {
        OptionMenu.SetActive(true);

        foreach(GameObject buttons in MainButtons)
        {
            buttons.SetActive(false);
        }
    }

    public void BackOptions()
    {
        OptionMenu.SetActive(false);
        foreach (GameObject buttons in MainButtons)
        {
            buttons.SetActive(true);
        }
    }
}
