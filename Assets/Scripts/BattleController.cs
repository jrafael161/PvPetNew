using System.Collections;
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
    GameObject PlayerObject, OponentObject;
    static bool action_done;
    static bool both_alive;
    List<bool> turns;
    int passedTurns;
    public int PlayerpassedTurns;
    public int OponentpassedTurns;
    float G_priority;
    bool Winner;
    static bool GameType;//true->PvP,false->PvE
    List<Item> PlayerActives;
    List<Item> OponentActives;
    Button back_or_capture_button;//back -> PvP, capture->
    Text battlelog,PlayerHpText,OponentHpText;
    public GameObject AbylityPrefab;
    Animator PlayerAni, OponentAni;
    Image PlayerHPBar, OponentHPBar;
    float HPBarUnit;
    float PlayerTotalHP, OponentTotalHP;

    private void Start()
    {
        Debug.Log("Instancia creada" + _instance);
        if (SceneManager.GetActiveScene().name == "CombatScreen")
        {
            GameType = true;
            StartBattle(true);
        }
            
    }

    public void StartBattle(bool gametype)
    {
        if (gametype)
        {
            GameType = true;
            if (SceneManager.GetActiveScene().name == "CombatScreen")
            {
                back_or_capture_button = FindObjectOfType<Button>();
                back_or_capture_button.gameObject.SetActive(false);
                GameObject Aux = GameObject.Find("BattleLog");
                battlelog = Aux.GetComponentInChildren<Text>();
            }
            PlayerObject = GameObject.Find("Player");//Why?
            OponentObject = GameObject.Find("Oponent");//Why?          
        }
        else
        {
            GameType = false;
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
        if (gametype)
        {
            InitializeHUD();
            SetCombatients();
            SetPets();
        }        
        battlelog.text = "Inicia batalla\n";
        GlobalControl.Instance.abilitiesHandler.BattleLog = battlelog;
        StartCoroutine("Battle");
    }

    void InitializeHUD()
    {
        GameObject playerHud, oponentHud;
        playerHud = GameObject.Find("PlayerHud");
        oponentHud = GameObject.Find("OponentHud");
        setPlayerHud(playerHud,Player,true);
        setPlayerHud(oponentHud,Oponent,false);
    }

    void setPlayerHud(GameObject pHud, PlayerData pData, bool whichPlayer)
    {
        Text[] txts = pHud.GetComponentsInChildren<Text>();
        //se asigna el battletag
        txts[0].text = pData.BattleTag;
        //se asigna el HP
        txts[1].text = pData.HP.ToString();
        if (whichPlayer)
        {
            PlayerHpText = txts[1];
            PlayerTotalHP = pData.HP;
        }
        else
        {
            OponentHpText = txts[1];
            OponentTotalHP = pData.HP;
        }
        Image[] imgs = pHud.GetComponentsInChildren<Image>();        
        //4 sprite del jugador en marco
        imgs[3].sprite = pData.PlayerSprite;
        //8 sprite de la mascota en marco
        imgs[6].sprite = PetDBManager.Instance.PetDB.Find(x => x.PetName == pData.CompanionPet.PetName).PetSprite;
        GameObject EquipedItem;
        GameObject EquipedItemAux;
        Image[] img;
        if (whichPlayer)
        {
            PlayerHPBar = imgs[2];
            float PlayerHPLenght = PlayerHPBar.sprite.rect.width;
            HPBarUnit = PlayerHPLenght / PlayerTotalHP;
            EquipedItem = GameObject.Find("PlayerHud/PlayerEquipedItems/Item_Frame");
        }
        else
        {
            OponentHPBar = imgs[2];
            EquipedItem = GameObject.Find("OponentHud/PlayerEquipedItems/Item_Frame");
        }
        foreach (Item item in pData.EquipedItems)
        {
            EquipedItemAux = Instantiate(EquipedItem) as GameObject;
            EquipedItemAux.SetActive(true);
            EquipedItemAux.transform.SetParent(EquipedItem.transform.parent, false);
            img = EquipedItemAux.GetComponentsInChildren<Image>();
            img[1].sprite = item.icon;
        }        
        EquipedItem.SetActive(false);
    }

    void SetCombatients()
    {
        GameObject playerChara, oponentChara;
        playerChara = GameObject.Find("Player");
        oponentChara = GameObject.Find("Oponent");
        Image[] sprites = playerChara.GetComponentsInChildren<Image>();
        foreach (Image img in sprites)
        {
            if (!Player.PlayerSpriteName.Contains(img.name))
            {
                img.gameObject.SetActive(false);
            }
        }
        sprites = oponentChara.GetComponentsInChildren<Image>();
        foreach (Image img in sprites)
        {
            if (!Oponent.PlayerSpriteName.Contains(img.name))
            {
                img.gameObject.SetActive(false);
            }
        }

        Player.PlayerAnimation = playerChara.GetComponentInChildren<Animator>();
        Oponent.PlayerAnimation = oponentChara.GetComponentInChildren<Animator>();
    }

    void SetPets()
    {
        GameObject playerPetSprite, oponentPetSprite;
        playerPetSprite = GameObject.Find("PlayerPet");
        oponentPetSprite = GameObject.Find("OponentPet");
        Image sprite = playerPetSprite.GetComponentInChildren<Image>();
        sprite.sprite = PetDBManager.Instance.PetDB.Find(x => x.PetName == Player.CompanionPet.PetName).PetSprite;
        sprite.SetNativeSize();
        sprite = oponentPetSprite.GetComponentInChildren<Image>();
        sprite.sprite = PetDBManager.Instance.PetDB.Find(x => x.PetName == Oponent.CompanionPet.PetName).PetSprite;
        sprite.SetNativeSize();
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
            yield break;
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
            yield break;            
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
            yield break;            
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
                        yield return new WaitForSeconds(2f);
                    }
                    else//Si la mascota del oponente esta muerta
                    {
                        Attack(player_onturn.playerPetasPlayer, player_notonturn);
                        ArePlayersAlive();
                        action_done = true;
                        battlelog.text = battlelog.text + player_onturn.BattleTag + " realizo su turno\n";
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
        bool whichPlayer = false;
        bool notPlayer = false;
        if (Attacker.BattleTag == Player.BattleTag)
        {
            whichPlayer = true;
        }
        else if (Attacker.BattleTag == Oponent.BattleTag)
        {
            whichPlayer = false;
        }
        else
        {
            notPlayer = true;
        }
        float crit_prob = Attacker.critic_prob * 100;
        float crit_chance = Mathf.Round(crit_prob);
        float  hit = 0;
        float trueDamage = 0;
        if (crit_chance >= Random.Range(0, 100))
        {
            trueDamage = ((Attacker.Strength * Attacker.EquipedGear[(int)BodyZone.Weapon].Value) * 2);
            if (Attacked.Armor > 0)
            {
                hit = trueDamage - (trueDamage * (Attacked.Armor / 100));
            }
            else
            {
                hit = trueDamage;
            }
            battlelog.text = battlelog.text + " " + Attacker.BattleTag + " hizo un golpe critico\n";
        }
        else
        {
            trueDamage = ((Attacker.Strength * Attacker.EquipedGear[(int)BodyZone.Weapon].Value));
            if (Attacked.Armor > 0)
            {
                hit = trueDamage - (trueDamage * (Attacked.Armor / 100));
            }
            else
            {
                hit = trueDamage;
            }            
        }
        hit = Mathf.Round(hit);
        float hit_prob = Attacker.Speed / Attacked.Agility;
        hit_prob = hit_prob * 100;
        float hit_chance = Mathf.Round(hit_prob);
        if ( hit_chance >= Random.Range(0, 100))
        {
            if (whichPlayer && !notPlayer)
            {
                Attacker.PlayerAnimation.Play(Attacker.PlayerSpriteName + "_Left_" + "Hit");
            }
            else if (!notPlayer)
            {
                Attacker.PlayerAnimation.Play(Attacker.PlayerSpriteName + "_Right_" + "Hit");
            }

            if (hit > Attacked.HP)
            {
                Attacked.HP = 0;
                if (!whichPlayer && !notPlayer)
                {
                    PlayerHpText.text = "0";
                    UpdateLifeBarLength(PlayerHPBar,whichPlayer);
                }
                else if(!notPlayer)
                {
                    OponentHpText.text = "0";
                    UpdateLifeBarLength(OponentHPBar, whichPlayer);
                }
            }
            else
            {
                Attacked.HP -= hit;
                if (!whichPlayer && !notPlayer)
                {
                    PlayerHpText.text = Attacked.HP.ToString();
                    UpdateLifeBarLength(PlayerHPBar, whichPlayer);
                }
                else if(!notPlayer)
                {
                    OponentHpText.text = Attacked.HP.ToString();
                    UpdateLifeBarLength(OponentHPBar, whichPlayer);
                }
            }            
            //Attacker.PlayerAnimation.Play(Attacker.PlayerSpriteName + "_Hit");
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

        if (GameType)
        {
            Player.PlayerAnimation = new Animator();
            Oponent.PlayerAnimation = new Animator();
            Player.PlayerAnimation = PlayerObject.GetComponentInChildren<Animator>();
            Oponent.PlayerAnimation = OponentObject.GetComponentInChildren<Animator>();
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

    public void UpdateLifeBarLength(Image HPBar, bool whichP)
    {
        if (!whichP)
        {

            PlayerHPBar.fillAmount = Player.HP / PlayerTotalHP;
        }
        else
        {
            OponentHPBar.fillAmount = Oponent.HP / OponentTotalHP;
        }
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

