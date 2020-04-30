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

    public void Initialize()
    {
        _instance = this;
    }

    public void SetPasives(PlayerData player,PlayerData oponent)
    {
        bool DontSetBuff = false;
        if (player.EquipedGear != null)
        {
            for (int i = 0; i < player.EquipedGear.Count; i++)//Checa habilidades de armadura y arma
            {
                if (player.EquipedGear[i] == null)
                {
                    DontSetBuff = true;
                    continue;
                }
                else
                {
                    if (!DontSetBuff)
                    {
                        if (player.EquipedGear.TrueForAll(x => x.Set == player.EquipedGear[0].Set))
                        {
                            UseAllSetBonus();
                        }
                        else
                        {
                            DontSetBuff = true;
                        }
                    }
                    if (player.EquipedGear[i].has_ability)
                    {
                        if (player.EquipedGear[i].At == AbiltyType.Passive)
                        {
                            if (player.EquipedGear[i].Abilitys.Eff == Effect.Oneself)
                            {
                                UsePasive(player, player.EquipedGear[i].Abilitys);
                            }
                            else if (player.EquipedGear[i].Abilitys.Eff == Effect.Opponent)
                            {
                                UsePasive(oponent, player.EquipedGear[i].Abilitys);
                            }
                        }
                    }
                }
            }
        }        
        if (player.EquipedItems != null)
        {
            for (int i = 0; i < player.EquipedItems.Count; i++)//Checa habilidades de los items equipados
            {
                if (player.EquipedItems[i] == null)
                {
                    continue;
                }
                if (player.EquipedItems[i].has_ability)
                {
                    if (player.EquipedItems[i].At == AbiltyType.Passive)
                    {
                        if (player.EquipedItems[i].Abilitys.Eff == Effect.Oneself)
                        {
                            UsePasive(player, player.EquipedItems[i].Abilitys);
                        }
                        else if (player.EquipedItems[i].Abilitys.Eff == Effect.Opponent)
                        {
                            UsePasive(oponent, player.EquipedItems[i].Abilitys);
                        }
                    }
                }
            }
        }            
    }

    public List<Item> SetActives(PlayerData player)
    {
        List<Item> itemsWActive = new List<Item>();
        if (player.EquipedGear != null)
        {
            for (int i = 0; i < player.EquipedGear.Count; i++)//Checa habilidades de armadura y arma
            {
                if (player.EquipedGear[i] == null)
                {
                    continue;
                }
                if (player.EquipedGear[i].has_ability)
                {
                    if (player.EquipedGear[i].At == AbiltyType.Active)
                    {
                        itemsWActive.Add(player.EquipedGear[i]);
                    }
                }
            }
        }
        if (player.EquipedItems != null)
        {
            for (int i = 0; i < player.EquipedItems.Count; i++)//Checa habilidades de armadura y arma
            {
                if (player.EquipedItems[i] == null)
                {
                    continue;
                }
                if (player.EquipedItems[i].has_ability)
                {
                    if (player.EquipedItems[i].At == AbiltyType.Active)
                    {
                        itemsWActive.Add(player.EquipedItems[i]);
                    }
                }
            }
        }
        return itemsWActive;
    }

    public List<Item> CheckTriggerCondition(List<Item> availableActives, PlayerData player, bool WhoIS)
    {
        List<Item> readyActives = new List<Item>();
        for (int i = 0; i < availableActives.Count; i++)
        {
            if (availableActives[i].Abilitys.Tt == ThresholdType.Turns)
            {
                if (WhoIS)//Si es jugador
                {
                    if (BattleController.Instance.PlayerpassedTurns >= availableActives[i].Abilitys.minThreshold && BattleController.Instance.PlayerpassedTurns <= availableActives[i].Abilitys.maxThreshold)
                    {
                        readyActives.Add(availableActives[i]);
                    }
                }                
                else if (BattleController.Instance.OponentpassedTurns >= availableActives[i].Abilitys.minThreshold && BattleController.Instance.OponentpassedTurns <= availableActives[i].Abilitys.maxThreshold)
                {
                    readyActives.Add(availableActives[i]);
                }
            }
            else if (availableActives[i].Abilitys.Tt == ThresholdType.Health)
            {
                if (player.HP <= availableActives[i].Abilitys.minThreshold)
                {
                    readyActives.Add(availableActives[i]);
                }
            }
        }
        return readyActives;
    }

    public void UseActive(PlayerData player ,Ability ability)
    {
        switch (ability.Et)
        {
            case EffectType.Damage:
                if (player == BattleController.Instance.Player)
                    player = BattleController.Instance.Oponent;
                else
                    player = BattleController.Instance.Player;
                Damage(player, ability);
                break;
            case EffectType.Healing:
                Heal(player, ability);
                break;
            case EffectType.Buff:
                Buff(player, ability);
                break;
            case EffectType.Debuff:
                if (player == BattleController.Instance.Player)
                    player = BattleController.Instance.Oponent;
                else
                    player = BattleController.Instance.Player;
                Debuff(player, ability);
                break;
            default:
                break;
        }
    }

    public void Damage(PlayerData player, Ability ability)
    {
        player.HP -= ability.Bonus;
        Debug.Log("Daño hecho a " + player.BattleTag + " de " + ability.Bonus + " por " + ability.Name);
    }

    public void Heal(PlayerData player, Ability ability)
    {
        player.HP += ability.Bonus;
        Debug.Log("Curacion hecha a " + player.BattleTag + " de " + ability.Bonus + " por " + ability.Name);
    }

    public void Buff(PlayerData player, Ability ability)
    {
        switch (ability.Bt)
        {
            case BuffType.Health:
                player.HP += ability.Bonus;
                Debug.Log(ability.Bonus + "en vida " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Strength:
                player.Strength += ability.Bonus;
                Debug.Log(ability.Bonus + "en fuerza " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Speed:
                player.Speed += ability.Bonus;
                Debug.Log(ability.Bonus + "en velicidad " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Agility:
                player.Agility += ability.Bonus;
                Debug.Log(ability.Bonus + "en agilidad " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Critic:
                player.critic_prob += ability.Bonus;
                Debug.Log(ability.Bonus + "en critico " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Armor:
                player.Armor += ability.Bonus;
                Debug.Log(ability.Bonus + "en armadura " + " a " + player.BattleTag + " por " + ability.name);
                break;
            default:
                Debug.Log("No se supo que hacer con la habilidad " + ability.name);
                break;
        }
    }

    public void Debuff(PlayerData player, Ability ability)
    {
        switch (ability.Bt)
        {
            case BuffType.Health:
                player.HP -= ability.Bonus;
                Debug.Log(ability.Bonus + "en vida " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Strength:
                player.Strength -= ability.Bonus;
                Debug.Log(ability.Bonus + "en fuerza " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Speed:
                player.Speed -= ability.Bonus;
                Debug.Log(ability.Bonus + "en velicidad " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Agility:
                player.Agility -= ability.Bonus;
                Debug.Log(ability.Bonus + "en agilidad " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Critic:
                player.critic_prob -= ability.Bonus;
                Debug.Log(ability.Bonus + "en critico " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Armor:
                player.Armor -= ability.Bonus;
                Debug.Log(ability.Bonus + "en armadura " + " a " + player.BattleTag + " por " + ability.name);
                break;
            default:
                Debug.Log("No se supo que hacer con la habilidad " + ability.name);
                break;
        }
    }

    public void UsePasive(PlayerData player, Ability ability)
    {
        switch (ability.Bt)
        {
            case BuffType.Health:
                player.HP += ability.Bonus;
                Debug.Log(ability.Bonus  + "en vida " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Strength:
                player.Strength += ability.Bonus;
                Debug.Log(ability.Bonus + "en fuerza " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Speed:
                player.Speed += ability.Bonus;
                Debug.Log(ability.Bonus + "en velicidad " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Agility:
                player.Agility += ability.Bonus;
                Debug.Log(ability.Bonus + "en agilidad " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Critic:
                player.critic_prob += ability.Bonus;
                Debug.Log(ability.Bonus + "en critico " + " a " + player.BattleTag + " por " + ability.name);
                break;
            case BuffType.Armor:
                player.Armor += ability.Bonus;
                Debug.Log(ability.Bonus + "en armadura " + " a " + player.BattleTag + " por " + ability.name);
                break;
            default:
                Debug.Log("No se supo que hacer con la habilidad " + ability.name);
                break;
        }    
    }

    public void UseAllSetBonus()
    {

    }
}
