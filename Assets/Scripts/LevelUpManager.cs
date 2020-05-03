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
        player = new PlayerData();
        GlobalControl.Instance.CopyPlayer(player);
    }

    public void StatUp(int stat)
    {
        switch (stat)
        {
            case 1:
                if (player.LevelUpPoints > 0)
                {
                    player.HP += 10;
                    player.Strength += 1;
                    player.LevelUpPoints -= 1;
                }                    
                break;
            case 2:
                if (player.LevelUpPoints > 0)
                {
                    player.HP += 10;
                    player.Agility += 1;
                    player.LevelUpPoints -= 1;
                }                    
                break;
            case 3:
                if (player.LevelUpPoints > 0)
                {
                    player.HP += 10;
                    player.Speed += 1;
                    player.LevelUpPoints -= 1;
                }                    
                break;
            default:
                break;
        }        
        GlobalControl.Instance.InitializePlayerData(player);
    }

    public void StatDown(int stat)
    {
        switch (stat)
        {
            case 1:
                if (player.Strength > GlobalControl.Instance.playeProfile.Strength)
                {
                    player.HP -= 10;
                    player.Strength -= 1;
                    player.LevelUpPoints += 1;
                }                    
                break;

            case 2:
                if (player.Agility > GlobalControl.Instance.playeProfile.Agility)
                {
                    player.HP -= 10;
                    player.Agility -= 1;
                    player.LevelUpPoints += 1;
                }                    
                break;
            case 3:
                if (player.Speed > GlobalControl.Instance.playeProfile.Speed)
                {
                    player.HP -= 10;
                    player.Speed -= 1;
                    player.LevelUpPoints += 1;
                }                    
                break;

            default:
                break;
        }        
        GlobalControl.Instance.InitializePlayerData(player);
    }

    public void SaveChanges()
    {
        GlobalControl.Instance.playeProfile.HP = player.HP;
        GlobalControl.Instance.playeProfile.Strength = player.Strength;
        GlobalControl.Instance.playeProfile.Speed = player.Speed;
        GlobalControl.Instance.playeProfile.Agility = player.Agility;
        GlobalControl.Instance.playeProfile.LevelUpPoints = player.LevelUpPoints;
        GlobalControl.Instance.SavePlayerData();
        GlobalControl.Instance.InitializePlayerData();
    }
}
