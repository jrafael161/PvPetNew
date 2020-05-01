using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelUpManager : MonoBehaviour
{
    static PlayerData player;
    public Button SaveChangesBtn;
    private void Start()
    {
        player = GlobalControl.Instance.playeProfile;
        SaveChangesBtn.gameObject.SetActive(true);
    }

    public void StatUp(int stat)
    {
        switch (stat)
        {
            case 1:
                player.Strength += 1;
                break;

            case 2:
                player.Agility += 1;
                break;

            case 3:
                player.Speed += 1;
                break;

            default:
                break;
        }
        player.LevelUpPoints -= 1;
        GlobalControl.Instance.InitializePlayerData(player);
    }

    public void StatDown(int stat)
    {
        switch (stat)
        {
            case 1:
                player.Strength += 1;
                break;

            case 2:
                player.Agility += 1;
                break;

            case 3:
                player.Speed += 1;
                break;

            default:
                break;
        }
        player.LevelUpPoints += 1;
        GlobalControl.Instance.InitializePlayerData(player);
    }

    public void SaveChanges()
    {
        GlobalControl.Instance.playeProfile.HP = player.HP;
        GlobalControl.Instance.playeProfile.Strength = player.Strength;
        GlobalControl.Instance.playeProfile.Speed = player.Speed;
        GlobalControl.Instance.playeProfile.Agility = player.Agility;
    }
}
