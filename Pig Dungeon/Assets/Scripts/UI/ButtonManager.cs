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

    public void StartButton()
    {
        SceneManager.LoadScene("");
    }
}
