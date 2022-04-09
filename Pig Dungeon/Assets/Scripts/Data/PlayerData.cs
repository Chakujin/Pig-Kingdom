using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int heal;
    public int diamonds;
    public int level;
    public float volumeMain;
    public bool gameStart;

    public PlayerData(GameManager manager)
    {
        diamonds = manager.playerDiamond;
        heal = manager.playerHeal;
        level = manager.level;
        volumeMain = manager.MainVolume;
        gameStart = manager.startGame;
    }
}
