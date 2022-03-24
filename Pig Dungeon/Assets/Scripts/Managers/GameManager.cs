using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //Ingame
    public static GameManager inst;
    public int playerHeal;
    public int playerDiamond;
    public int level;

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
    }
}
