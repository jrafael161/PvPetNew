using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleController : MonoBehaviour
{
    GlobalControl gc;
    PlayerData Player, Oponent;    
    float Ptemp_critic_prob;
    static bool action_done;
    static bool both_alive;
    List<bool> turns;
    float G_priority;

    private void Start()
    {
        PlayerData Winner;
        gc = FindObjectOfType<GlobalControl>();
        action_done = true;
        both_alive = true;
        SetPlayersData();
        StartCoroutine(Battle());
    }

    private void Update()
    {         
    }

    IEnumerator Battle()
    {
        AbilitiesHandler.Instance.SetPasives(Player,Oponent);
        AbilitiesHandler.Instance.SetPasives(Oponent,Player);
        while (Player.HP > 0 && Oponent.HP > 0)
        {            
            set_turns(Player.Speed/Oponent.Speed);
            for (int i = 0; i < turns.Count; i++)
            {
                if (action_done)
                {
                    if (turns[i] == true && both_alive)
                    {
                        action_done = false;
                        yield return StartCoroutine(action(Player));                        
                    }
                    else if(both_alive)
                    {
                        action_done = false;
                        yield return StartCoroutine(action(Oponent));                        
                    }
                }                
            }
        }
        if (Player.HP > Oponent.HP)
            Debug.Log(Player.BattleTag + " ha ganado");
        else
            Debug.Log(Oponent.BattleTag + " ha ganado");
        Debug.Log("Termino el combate");
        yield return true;
    }

    void set_turns(float priority)
    {
        if (G_priority == priority)//Checa su ha cambiado la velocidad de jugadores para rehacer la fila de turnos
        {
            return;
        }
        if (priority == 1)//Si tienen la misma velocidad
        {
            turns = new List<bool>(2);
            if (Player.Agility == Oponent.Agility)//Si tienen la misma agilidad
            {
                if (Random.Range(0, 100) >= 50)//Se deja a la suerta quien va primero
                {
                    turns.Add(true);
                    turns.Add(false);
                }
                else
                {
                    turns.Add(false);
                    turns.Add(true);                    
                }
            }
            else//De lo contrario el que tenga mayor agilidad va primero
            {
                if (Player.Agility > Oponent.Agility)
                {
                    turns.Add(true);
                    turns.Add(false);
                }
                else
                {
                    turns.Add(false);
                    turns.Add(true);
                }
            }
            G_priority = priority;
            return;
        }
        int turn_ratio;
        if (priority<0)
        {
            float aux = Mathf.Pow(priority, -1);
            turn_ratio = Mathf.FloorToInt(priority) + 1;
        }
        else
        {
            turn_ratio = Mathf.FloorToInt(priority) + 1;
        }            
        turns = new List<bool>(turn_ratio);
        for (int i = 0, t = turn_ratio; i < t+1; i++)
        {
            if (priority > 0)
            {
                if (turn_ratio > 0)
                {
                    turns.Add(true);
                    turn_ratio--;
                }
                else
                {
                    turns.Add(false);
                }
            }
            else
            {
                if (turn_ratio > 0)
                {
                    turns.Add(false);
                    turn_ratio--;
                }
                else
                {
                    turns.Add(true);
                }
            }
        }
        G_priority = priority;
    }

    IEnumerator action(PlayerData player_onturn)
    {
        if (player_onturn.PlayerID == Player.PlayerID)
        {
            for (int i = 0; i < player_onturn.EquipedItems.Count; i++)
            {
                if (player_onturn.EquipedItems[i].It == ItemType.Active)
                {
                    if(AbilitiesHandler.Instance.CheckTriggerCondition(player_onturn.EquipedItems[i]))
                        AbilitiesHandler.Instance.UseActive(player_onturn.EquipedItems[i]);
                }
            }
            for (int i = 0; i < player_onturn.OwnedAbilities.Count; i++)
            {
                if (player_onturn.OwnedAbilities[i].It == ItemType.Active)
                {
                    if(AbilitiesHandler.Instance.CheckTriggerCondition(player_onturn.OwnedAbilities[i]))
                        AbilitiesHandler.Instance.UseActive(player_onturn.OwnedAbilities[i]);
                }
            }
            Attack(Player,Oponent);

            if (Oponent.HP <= 0)//Hacer una funcion de Check health
            {
                both_alive = false;
                action_done = true;
                yield return null;
            }
            Debug.Log(player_onturn.BattleTag + " realizo su turno");
        }
        else
        {
            //UseActives()
            Attack(Oponent, Player);

            if (Player.HP <= 0)
            {
                both_alive = false;
                action_done = true;
                yield return null;
            }            
            Debug.Log(player_onturn.BattleTag + " realizo su turno");
        }
        yield return new WaitForSeconds(2f);//Reemplazar con el tiempo que tome la accion que realizara el jugador
        action_done = true;
    }

    public void Attack(PlayerData Attacker, PlayerData Attacked)
    {
        float crit_prob = Attacker.critic_prob * 100;
        int crit_chance = Mathf.FloorToInt(crit_prob);
        int  hit = 0;
        if (crit_chance >= Random.Range(0, 100))
        {
            hit = ((Attacker.Strength * Attacker.Weapon.damage) * 2) / Attacked.Armor;
        }
        else
        {
            hit = (Attacker.Strength * Attacker.Weapon.damage) / Attacked.Armor;
        }
        hit = Mathf.FloorToInt(hit);

        float hit_prob = Attacker.Speed / Attacked.Agility;
        int hit_chance = Mathf.FloorToInt(hit_prob) * 100;
        if ( hit_chance >= Random.Range(0, 100))
        {
            Debug.Log(Attacker.BattleTag + " le hizo " + hit + "puntos de daño a " + Attacked.BattleTag + "con su ataque");
            Attacked.HP -= hit;
        }
        else
        {
            Debug.Log( Attacker.BattleTag + " fallo su ataque");
        }        
    }

    public void SetPlayersData()
    {
        Player = gc.playeProfile;
        //Oponent = gc.oponentProfile;
        Oponent = gc.playeProfile;//El oponente no tiene armas soo
    }

    public void SetBuffs()
    {
        Debug.Log("Se aplicaron buffs");
    }
}

