using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseManager : MonoBehaviour
{
    public static bool gameispaused = false;
    public bool paused;
    private GameObject pausemenuUI;
    private GameManager m_gameManager;
    //public GameObject ingameMenu;

    private void Awake()
    {
        pausemenuUI = GameObject.FindGameObjectWithTag("PauseCanvas");
        m_gameManager = GameObject.FindGameObjectWithTag("gameManager").GetComponent<GameManager>();

        pausemenuUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (gameispaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void Resume()
    {
        pausemenuUI.SetActive(false);
        //ingameMenu.SetActive(false);
        Cursor.visible = false;
        Time.timeScale = 1f;

        gameispaused = false;
        m_gameManager.isPaused = false;

        paused = false;
    }

    public void Pause()
    {
        pausemenuUI.SetActive(true);
        Time.timeScale = 0f;
        gameispaused = true;

        m_gameManager.isPaused = true;
        paused = true;
    }
}
