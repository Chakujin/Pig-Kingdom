using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerData
{
    public int heal;
    public int diamonds;
    public int level;

    public PlayerData(GameManager manager)
    {
        diamonds = manager.playerDiamond;
        heal = manager.playerHeal;
        level = manager.level;

    }
}
