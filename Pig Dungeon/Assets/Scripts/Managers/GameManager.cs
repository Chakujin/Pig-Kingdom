using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Other
    public bool isPaused = false;

    //Ingame
    public static GameManager inst;
    public int playerHeal;
    public int playerDiamond;
    public int level;
    public float MainVolume = 1f;
    public bool startGame;

    private void Awake()
    {
        if(GameManager.inst == null)
        {
            GameManager.inst = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Call this to save
    public void saveGame()
    {
        SaveSystem.saveGame(this);
    }

    //Call this to load
    public void loadGame()
    {
        PlayerData data = SaveSystem.loadGame();

        level = data.level;
        playerHeal = data.heal;
        playerDiamond = data.diamonds;
        MainVolume = data.volumeMain;
        startGame = data.gameStart;
    }
}
