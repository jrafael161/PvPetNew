using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    GlobalControl gc;
    PlayerData Player, Oponent;
    int PlayerTempHp, OponentTempHp;

    private void Start()
    {
        gc = FindObjectOfType<GlobalControl>();
        SetPlayersData();
    }

    public PlayerData Battle()
    {
        while (PlayerTempHp > 0 && OponentTempHp > 0)
        {

        }
        return Player;
    }

    public void SetPlayersData()
    {
        Player = gc.playeProfile;
        Oponent = gc.oponentProfile;
    }

    public void GetPlayers()
    {
        Player = gc.GetPlayer(true);
        Oponent = gc.GetPlayer(false);
    }
}

