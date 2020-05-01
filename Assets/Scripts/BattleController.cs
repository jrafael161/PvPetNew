using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    List<Item> PlayerActives;
    List<Item> OponentActives;
    public GameObject back_button;
    public GameObject buttoncapture;
    public Text battlelog;



    private void Start()
    {
        Debug.Log("Instancia creada"+_instance);   
    }

    public void StartBattle(bool gametype)
    {
        action_done = true;
        both_alive = true;
        SetPlayersData();
        PlayerActives = new List<Item>();
        OponentActives = new List<Item>();
        battlelog.text = "Inicia batalla\n";
        StartCoroutine("Battle");
    }


    IEnumerator Battle()
    {
        SetPlayersData();
        CalculateArmor(Player);
        CalculateArmor(Oponent);
        AbilitiesHandler.Instance.SetPasives(Player, Oponent);
        AbilitiesHandler.Instance.SetPasives(Oponent,Player);
        PlayerActives = AbilitiesHandler.Instance.SetActives(Player);
        OponentActives = AbilitiesHandler.Instance.SetActives(Oponent);

        while (Player.HP > 0 && Oponent.HP > 0)
        {            
            set_turns((float)Player.Speed/ (float)Oponent.Speed);
            for (int i = 0; i < turns.Count; i++)
            {
                if (action_done)
                {
                    if (turns[i] == true && both_alive)
                    {
                        action_done = false;
                        yield return StartCoroutine(action(Player));
                        passedTurns++;
                        PlayerpassedTurns++;
                    }
                    else if(both_alive)
                    {
                        action_done = false;
                        yield return StartCoroutine(action(Oponent));
                        passedTurns++;
                        OponentpassedTurns++;
                    }
                }                
            }
        }
        if (Player.HP > Oponent.HP)
            battlelog.text = battlelog.text + Player.BattleTag + " ha ganado\n";
        else
            battlelog.text = battlelog.text + Oponent.BattleTag + " ha ganado\n";

        back_button.SetActive(true);
        battlelog.text = battlelog.text + "Termino el combate\n";
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
        if (priority<1)
        {
            float aux = Mathf.Pow(priority, -1);
            turn_ratio = Mathf.FloorToInt(aux);// + 1;
        }
        else
        {
            turn_ratio = Mathf.FloorToInt(priority);// + 1;
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
        List<Item> UsableActives = new List<Item>();
        if (player_onturn.PlayerID == Player.PlayerID)
        {

            UsableActives = AbilitiesHandler.Instance.CheckTriggerCondition(PlayerActives,Player, true);
            for (int i = 0; i < UsableActives.Count; i++)
            {
                AbilitiesHandler.Instance.UseActive(Player,UsableActives[i].Abilitys);
            }            
            Attack(Player,Oponent);
            UsableActives = AbilitiesHandler.Instance.CheckTriggerCondition(OponentActives, Oponent, false);//Se le da oportunidad al oponente de responder
            for (int i = 0; i < UsableActives.Count; i++)
            {
                if (UsableActives[i].Abilitys.Et == EffectType.Healing)
                {
                    AbilitiesHandler.Instance.UseActive(Player, UsableActives[i].Abilitys);
                }                    
            }
            if (Oponent.HP <= 0)//Hacer una funcion de Check health
            {
                both_alive = false;
                action_done = true;
                yield return null;
            }
            battlelog.text = battlelog.text + Player.BattleTag + " realizo su turno\n";
        }
        else
        {
            UsableActives = AbilitiesHandler.Instance.CheckTriggerCondition(OponentActives,Oponent, false);
            for (int i = 0; i < UsableActives.Count; i++)
            {
                AbilitiesHandler.Instance.UseActive(Oponent, UsableActives[i].Abilitys);
            }
            Attack(Oponent, Player);
            UsableActives = AbilitiesHandler.Instance.CheckTriggerCondition(PlayerActives, Player, true);
            for (int i = 0; i < UsableActives.Count; i++)
            {
                if (UsableActives[i].Abilitys.Et == EffectType.Healing)
                {
                    AbilitiesHandler.Instance.UseActive(Oponent, UsableActives[i].Abilitys);
                }                
            }
            if (Player.HP <= 0)
            {
                both_alive = false;
                action_done = true;
                yield return null;
            }
            battlelog.text = battlelog.text + Oponent.BattleTag + " realizo su turno\n";
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
            hit = ((Attacker.Strength * Attacker.EquipedGear[(int)BodyZone.Weapon].Value) * 2) / Attacked.Armor;
        }
        else
        {
            hit = (Attacker.Strength * Attacker.EquipedGear[(int)BodyZone.Weapon].Value) / Attacked.Armor;
        }
        hit = Mathf.FloorToInt(hit);

        float hit_prob = (float)Attacker.Speed / (float)Attacked.Agility;
        hit_prob = hit_prob * 100;
        int hit_chance = Mathf.FloorToInt(hit_prob);
        if ( hit_chance >= Random.Range(0, 100))
        {
            battlelog.text = battlelog.text + Attacker.BattleTag + " le hizo " + hit + " puntos de daño a " + Attacked.BattleTag + "con su ataque\n";
            battlelog.text = battlelog.text + "Le quedan " + Attacked.HP + " puntos de vida\n";
            Attacked.HP -= hit;
        }
        else
        {
            battlelog.text = battlelog.text+Attacker.BattleTag + " fallo su ataque\n";
        }        
    }

    public void SetPlayersData()
    {
        Player = GlobalControl.Instance.playeProfile;
        Oponent = GlobalControl.Instance.oponentProfile;       
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

