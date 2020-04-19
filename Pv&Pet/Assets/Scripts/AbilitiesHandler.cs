using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesHandler
{
    private static AbilitiesHandler _instance;
    public static AbilitiesHandler Instance
    {
        get { return _instance; }
    }

    void Awake()
    {
        _instance = this;
    }

    public void SetPasives(PlayerData player,PlayerData oponent)
    {
        for (int i = 0; i < player.EquipedItems.Count; i++)
        {
            if (player.EquipedItems[i].It == ItemType.Pasive)
            {
                UsePasive(player, player.EquipedItems[i]);
            }
        }
        for (int i = 0; i < player.OwnedAbilities.Count; i++)
        {
            if (player.OwnedAbilities[i].It == ItemType.Pasive)
            {
                if (player.OwnedAbilities[i].Eff == Effect.Oneself)
                {
                    UsePasive(player, player.OwnedAbilities[i]);
                }
                else if(player.OwnedAbilities[i].Eff == Effect.Opponent)
                {
                    UsePasive(oponent, player.OwnedAbilities[i]);
                }
                
            }
        }
    }

    public bool CheckTriggerCondition(Item it)
    {
        return true;//Se cumplen las condiciones para que la abilidad sea activada
    }

    public bool CheckTriggerCondition(Ability ab)
    {
        return true;//Se cumplen las condiciones para que la abilidad sea activada
    }

    public void UseActive(Item it)
    {
        switch (it.Bt)
        {
            case BonusType.Health:

                break;
            case BonusType.Strength:
                break;
            case BonusType.Speed:
                break;
            case BonusType.Agility:
                break;
            case BonusType.Armor:
                break;
            case BonusType.NA:
                break;
            default:
                break;
        }
    }

    public void UseActive(Ability ab)
    {

    }

    public void UsePasive(PlayerData player, Item item)
    {
        switch (item.It)
        {
            case ItemType.Armor:
                break;

            case ItemType.Weapon:
                break;

            case ItemType.Pasive:
                break;

            case ItemType.Active:
                break;
            default:
                break;
        }
    }

    public void UsePasive(PlayerData player, Ability ability)
    {
        switch (ability.Bt)
        {
            case BuffType.Health:
                player.HP += ability.cuantity;
                break;
            case BuffType.Strength:
                player.Strength += ability.cuantity;
                break;
            case BuffType.Speed:
                player.Speed += ability.cuantity;
                break;
            case BuffType.Agility:
                player.Agility += ability.cuantity;
                break;
            case BuffType.Critic:
                player.critic_prob += ability.cuantity;
                break;
            case BuffType.Armor:
                player.Armor += ability.cuantity;
                break;
            default:
                break;
        }    
    }
}
