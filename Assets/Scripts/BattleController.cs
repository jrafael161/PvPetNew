﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class BattleController : MonoBehaviour
{

    private static BattleController _instance;

    public static BattleController Instance
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
    
    public PlayerData Player, Oponent;    
    static bool action_done;
    static bool both_alive;
    List<bool> turns;
    public int passedTurns;
    public int PlayerpassedTurns;
    public int OponentpassedTurns;
    float G_priority;
    public bool Winner;
    public static bool GameType;//true->PvP,false->PvE
    List<Item> PlayerActives;
    List<Item> OponentActives;
    public Button back_or_capture_button;//back -> PvP, capture->
    public Text battlelog;

    private void Start()
    {
        Debug.Log("Instancia creada" + _instance);
        if (SceneManager.GetActiveScene().name == "CombatScreen")
            StartBattle(true);
    }

    public void StartBattle(bool gametype)
    {
        if (gametype)
        {            
            if (SceneManager.GetActiveScene().name == "CombatScreen")
            {
                back_or_capture_button = FindObjectOfType<Button>();
                back_or_capture_button.gameObject.SetActive(false);
            }            
        }
        else
        {
            GameObject panel = GameObject.Find("pnl_mision");
            back_or_capture_button = panel.GetComponentInChildren<Button>();
            back_or_capture_button.gameObject.SetActive(false);
        }        
        action_done = true;
        both_alive = true;
        passedTurns = 0;
        PlayerpassedTurns = 0;
        OponentpassedTurns = 0;
        SetPlayersData();
        Player.PlayerActiveAbilities = new List<Item>();
        Oponent.PlayerActiveAbilities = new List<Item>();
        battlelog.text = "Inicia batalla\n";
        GlobalControl.Instance.abilitiesHandler.BattleLog = battlelog;
        StartCoroutine("Battle");
    }


    IEnumerator Battle()
    {
        CalculateArmor(Player);
        CalculateArmor(Oponent);
        AbilitiesHandler.Instance.SetPasives(Player, Oponent);
        AbilitiesHandler.Instance.SetPasives(Oponent,Player);
        Player.PlayerActiveAbilities = AbilitiesHandler.Instance.SetActives(Player);
        Oponent.PlayerActiveAbilities = AbilitiesHandler.Instance.SetActives(Oponent);

        while (Player.HP > 0 && Oponent.HP > 0)
        {            
            set_turns(Player.Speed/ Oponent.Speed);
            for (int i = 0; i < turns.Count; i++)
            {
                if (action_done)
                {
                    if (turns[i] == true && both_alive)
                    {
                        action_done = false;
                        yield return StartCoroutine("action",Player);
                        passedTurns++;
                        PlayerpassedTurns++;
                    }
                    else if(both_alive)
                    {
                        action_done = false;
                        yield return StartCoroutine("action",Oponent);
                        passedTurns++;
                        OponentpassedTurns++;
                    }
                }                
            }
        }
        if (Player.HP > Oponent.HP)
        {
            battlelog.text = battlelog.text + Player.BattleTag + " ha ganado\n";
            Winner = true;
            GiveXP();
        }
        else
        {
            battlelog.text = battlelog.text + Oponent.BattleTag + " ha ganado\n";
            Winner = false;
            GiveXP();
        }
        if (GameType)//pvp
        {
            back_or_capture_button.gameObject.SetActive(true);
            if (Winner)
            {
                GlobalControl.Instance.playeProfile.Wins += 1;
                //GlobalControl.Instance.playeProfile.PvPCoin += passedTurns * 10;
            }
            else
            {
                GlobalControl.Instance.playeProfile.Wins += 1;
                //GlobalControl.Instance.playeProfile.PvPCoin += passedTurns * 2;
            }
        }
        else//pve
        {
            if (Winner)
            {
                back_or_capture_button.gameObject.SetActive(true);
                //GlobalControl.Instance.playeProfile.PetCoin += passedTurns * 10;
            }
            //else
            //    GlobalControl.Instance.playeProfile.PetCoin += passedTurns * 2;
        }
        battlelog.text = battlelog.text + "Termino el combate\n";
        yield return true;
    }

    void GiveXP()
    {
        float XP = 0;
        int coins = 0;
        if (Winner)
        {
            XP = (((Player.HP * 100) / GlobalControl.Instance.playeProfile.HP) / passedTurns) / Player.Level;
            XP = Mathf.Floor(XP);            
            GlobalControl.Instance.playeProfile.XP += XP;            
            battlelog.text = battlelog.text + " " + "El jugador gano " + XP + " puntos de Xp\n";
            if (GameType)
            {
                coins = Mathf.FloorToInt(((Player.HP * 100) / GlobalControl.Instance.playeProfile.HP) / passedTurns);
                GlobalControl.Instance.playeProfile.PvPCoin += coins;
                battlelog.text = battlelog.text + " " + "El jugador gano " + coins + " monedas de PvP\n";
            }
            else
            {
                coins = Mathf.FloorToInt(((Player.HP * 100) / GlobalControl.Instance.playeProfile.HP) / passedTurns);
                GlobalControl.Instance.playeProfile.PetCoin += coins;
                battlelog.text = battlelog.text + " " + "El jugador gano " + coins + " monedas de Pet\n";
            }
        }
        else
        {
            XP = (passedTurns + Player.Level)/Player.Level;
            XP = Mathf.Floor(XP);
            GlobalControl.Instance.playeProfile.XP += XP;
            battlelog.text = battlelog.text + " " + "El jugador gano " + XP + " puntos de Xp\n";
        }
        GlobalControl.Instance.CheckIfLevelUP();
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
        if (priority<1)
        {
            float aux = Mathf.Pow(priority, -1);
            turn_ratio = Mathf.RoundToInt(aux);// + 1;
        }
        else
        {
            turn_ratio = Mathf.RoundToInt(priority);// + 1;
        }            
        turns = new List<bool>(turn_ratio);
        for (int i = 0, t = turn_ratio; i < t+1; i++)
        {
            if (priority > 1)
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
        PlayerData player_notonturn;
        if (player_onturn == Player)//El jugador en turno es el jugador
        {
            player_notonturn = Oponent;
        }
        else
        {
            player_notonturn = Player;
        }
        List<Item> UsableActives = new List<Item>();
        Attack(player_onturn, player_notonturn);//Ataque del jugador en turno
        if (!ArePlayersAlive())//Checa si alguien se murio despues del ataque
        {
            action_done = true;
            battlelog.text = battlelog.text + player_onturn.BattleTag + " realizo su turno\n";
            //yield break;
            yield return new WaitForSeconds(2f);
        }
        UsableActives = AbilitiesHandler.Instance.CheckTriggerCondition(player_onturn.PlayerActiveAbilities, Player, true);
        for (int i = 0; i < UsableActives.Count; i++)
        {
            AbilitiesHandler.Instance.UseActive(player_onturn, UsableActives[i].Abilitys);//Activas del jugador en turno
        }        
        if (!ArePlayersAlive())//Checa si alguien se murio despues de las activas
        {
            action_done = true;
            battlelog.text = battlelog.text + player_onturn.BattleTag + " realizo su turno\n";
            //yield break;
            yield return new WaitForSeconds(2f);
        }
        UsableActives = AbilitiesHandler.Instance.CheckTriggerCondition(player_notonturn.PlayerActiveAbilities, player_notonturn, false);//Se le da oportunidad al oponente de responder con sus activas de curacion
        for (int i = 0; i < UsableActives.Count; i++)
        {
            if (UsableActives[i].Abilitys.Et == EffectType.Healing)
            {
                AbilitiesHandler.Instance.UseActive(player_notonturn, UsableActives[i].Abilitys);//Usa sus activas
            }
        }
        if (!ArePlayersAlive())//Checa si alguien se murio despues de las activas
        {
            action_done = true;
            battlelog.text = battlelog.text + player_onturn.BattleTag + " realizo su turno\n";
            //yield break;
            yield return new WaitForSeconds(2f);
        }
        if (player_onturn.CompanionPet != null)//miniturno de la mascota
        {
            if (player_onturn.playerPetasPlayer.HP > 0)//Si la mascota del jugador en turno esta viva
            {
                if (player_notonturn.CompanionPet != null)//Si el oponente tiene mascota
                {
                    if (player_notonturn.playerPetasPlayer.HP > 0)//Si la mascota del oponente esta viva
                    {
                        Attack(player_onturn.playerPetasPlayer, player_notonturn.playerPetasPlayer);
                        action_done = true;
                        battlelog.text = battlelog.text + player_onturn.BattleTag + " realizo su turno\n";
                        //yield break;
                        yield return new WaitForSeconds(2f);
                    }
                    else//Si la mascota del oponente esta muerta
                    {
                        Attack(player_onturn.playerPetasPlayer, player_notonturn);
                        ArePlayersAlive();
                        action_done = true;
                        battlelog.text = battlelog.text + player_onturn.BattleTag + " realizo su turno\n";
                        //yield break;
                        yield return new WaitForSeconds(2f);
                    }
                }
            }
        }
        yield return new WaitForSeconds(2f);//Reemplazar con el tiempo que tome la accion que realizara el jugador
        action_done = true;
    }

    public void Attack(PlayerData Attacker, PlayerData Attacked)
    {
        float crit_prob = Attacker.critic_prob * 100;
        float crit_chance = Mathf.Round(crit_prob);
        float  hit = 0;
        float trueDamage = 0;
        if (crit_chance >= Random.Range(0, 100))
        {
            trueDamage = ((Attacker.Strength * Attacker.EquipedGear[(int)BodyZone.Weapon].Value) * 2);
            hit = trueDamage - (trueDamage * (Attacked.Armor / 100));
            battlelog.text = battlelog.text + " " + Attacker.BattleTag + " hizo un golpe critico\n";
        }
        else
        {
            trueDamage = ((Attacker.Strength * Attacker.EquipedGear[(int)BodyZone.Weapon].Value));
            hit = trueDamage - (trueDamage * (Attacked.Armor / 100));
        }
        hit = Mathf.Round(hit);
        float hit_prob = Attacker.Speed / Attacked.Agility;
        hit_prob = hit_prob * 100;
        float hit_chance = Mathf.Round(hit_prob);
        if ( hit_chance >= Random.Range(0, 100))
        {                        
            if (hit > Attacked.HP)
            {
                Attacked.HP = 0;
            }
            else
                Attacked.HP -= hit;
            battlelog.text = battlelog.text + Attacker.BattleTag + " le hizo " + hit + " puntos de daño a " + Attacked.BattleTag + " con su ataque\n";
            battlelog.text = battlelog.text + "Le quedan " + Attacked.HP + " puntos de vida\n";            
        }
        else
        {
            battlelog.text = battlelog.text + Attacker.BattleTag + " fallo su ataque\n";
        }
        ArePlayersAlive();
    }

    public void SetPlayersData()
    {
        Player = new PlayerData();
        Player.EquipedGear = new List<Item>();
        Player.EquipedItems = new List<Item>();
        Player.CompanionPet = ScriptableObject.CreateInstance("Pet") as Pet;        
        GlobalControl.Instance.CopyPlayer(Player);

        Player.playerPetasPlayer = new PlayerData();
        Player.playerPetasPlayer.EquipedGear = new List<Item>();
        Player.playerPetasPlayer.EquipedItems = new List<Item>();
        SetPet(Player.playerPetasPlayer, Player);

        Oponent = GlobalControl.Instance.oponentProfile;

        if (Oponent.CompanionPet != null)
        {
            Oponent.playerPetasPlayer = new PlayerData();
            Oponent.playerPetasPlayer.EquipedGear = new List<Item>();
            Oponent.playerPetasPlayer.EquipedItems = new List<Item>();
            SetPet(Oponent.playerPetasPlayer, Oponent);
        }
    }

    public void SetPet(PlayerData petAsPlayer,PlayerData player)
    {
        petAsPlayer.BattleTag = player.CompanionPet.PetName;
        petAsPlayer.HP = player.CompanionPet.HP;
        petAsPlayer.Level = player.CompanionPet.Level;
        petAsPlayer.XP = player.CompanionPet.XP;
        petAsPlayer.Strength = player.CompanionPet.Strength;
        petAsPlayer.Speed = player.CompanionPet.Speed;
        petAsPlayer.Agility = player.CompanionPet.Agility;
        petAsPlayer.Armor = player.CompanionPet.Armor;
        petAsPlayer.critic_prob = player.CompanionPet.critic_prob;
        //petAsPlayer.EquipedItems.Add(player.CompanionPet.PetItem);
        petAsPlayer.Inventory = null;

        petAsPlayer.EquipedGear = new List<Item>(4);
        for (int i = 0; i < 5; i++)
        {
            petAsPlayer.EquipedGear.Add(null);
        }
        petAsPlayer.EquipedGear[4] = ItemsDBmanager.Instance.ItemDB.Find(x => x.Name == "Pet Sword");//Equipa la pet sword        
    }

    public bool ArePlayersAlive()
    {
        if (Player.HP <= 0 || Oponent.HP <= 0)
        {
            both_alive = false;
            return false;
        }
        else
        {
            return true;
        }            
    }

    public void CalculateArmor(PlayerData player)
    {
        for (int i = 0; i < player.EquipedGear.Count - 1; i++)
        {
            if (player.EquipedGear[i] == null)
            {
                continue;
            }
            else
            {
                player.Armor += player.EquipedGear[i].Value;
            }                
        }
    }
}

