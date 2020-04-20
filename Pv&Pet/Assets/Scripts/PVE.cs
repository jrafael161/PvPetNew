using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.EventSystems;
using Firebase;
using Firebase.Database;
using Firebase.Unity.Editor;
using UnityEngine.SceneManagement;
using System;
using Random2 = System.Random;

public class PVE : MonoBehaviour
{
    DatabaseReference reference;

    public GameObject Panelact1;
    public GameObject Panelact2;
    public GameObject Panelact3;
    public GameObject Panelact4;
    public GameObject Panelact5;
    public GameObject Panelpreparativos;
    public GameObject Panelmision;
    public Transform dropdownMenu;

    public int Acto;
    public int Capitulo;
    public string titulo;

    private int hpuser;
    private int struser;
    private int aguser;
    private int speuser;
    private int armuser;
    private int expuser;
    private int available;

    public Sprite Bull;
    public Sprite LunarButterfly;
    public Sprite Mantis;
    public Sprite Roach;
    public Sprite Scarab;
    public Sprite Tick;
    public Sprite Caterpillar;
    public Sprite GiantBugCentipede;
    public Sprite GiantBugDeathWorm;
    public Sprite InsectsDragon;
    public Sprite RedAntKnight;
    public Sprite Waterstrider;
    public Sprite BlackAntArcher;
    public Sprite BlackAntBerserker;
    public Sprite BlackAntKnight;
    public Sprite BlackAntMage;
    public Sprite BlackAntProtector;
    public Sprite Golem;
    public Sprite DeathWorm;
    public Sprite GiantBugHercules;
    public Sprite HellMantis;
    public Sprite Swarm;
    public Sprite TitanTellia;
    public Sprite Tridentpupa;
    public Sprite DryadMini;
    public Sprite EarthDragon;
    public Sprite ForestSpider;
    public Sprite ImperialWidow;
    public Sprite SixWingFairy;
    public Sprite FeralKitsune;
    public Sprite RabbitWarriorsArcher;
    public Sprite RabbitWarriorsBandit;
    public Sprite RabbitWarriorsKnight;
    public Sprite SevenSinsGreed;
    public Sprite WindSnake;
    public Sprite Deer;
    public Sprite Elf_Assasin;
    public Sprite ElvesRapier;
    public Sprite ElvesRogueElf;
    public Sprite ElvesSpellcaster;
    public Sprite FairyFilia;
    public Sprite ArcaneGolem;
    public Sprite GemstoneFire;
    public Sprite GemstoneThunder;
    public Sprite GemstoneWater;
    public Sprite GemstoneWind;
    public Sprite OrbFire;
    public Sprite OrbFrost;
    public Sprite OrbThunder;
    public Sprite OrbWind;
    public Sprite ElementalEarthSpiritTellia;
    public Sprite ElementalGoddessAiri;
    public Sprite ElementalGoddessFlora;
    public Sprite ElementalGoddessimp;
    public Sprite ElementalGoddessYukia;
    public Sprite ElementalIceSpiritHelida;
    public Sprite ElementalSpiritFireBlazia;
    public Sprite ElementalWindSpiritTempestia;
    public Sprite DragonKingBlue;
    public Sprite DragonKingBrown;
    public Sprite DragonKingGreen;
    public Sprite DragonKingRed;
    public Sprite EgyptArcher;
    public Sprite EgyptAxe;
    public Sprite EgyptChariot;
    public Sprite EgyptKnight;
    public Sprite EgyptMage;
    public Sprite Hieracosphinx;
    public Sprite Cobra;
    public Sprite Crocodile;
    public Sprite Mummy;
    public Sprite Sphinx;
    public Sprite PirateBandit;
    public Sprite PirateCaptain;
    public Sprite PirateMagicScimitar;
    public Sprite PirateMonkey;
    public Sprite PirateParrot;
    public Sprite PirateSkeleton;
    public Sprite TurtleGolem;
    public Sprite Mermaid;
    public Sprite MermaidWarriorArliette;
    public Sprite MermaidWarriorSasha;
    public Sprite MermaidWarriorSion;
    public Sprite Octopus;
    public Sprite Piranos;
    public Sprite Shark;
    public Sprite TitanAquos;
    public Sprite SkeletonArcher;
    public Sprite SkeletonDragon;
    public Sprite SkeletonKnight;
    public Sprite SkeletonKnightBaron;
    public Sprite SkeletonMage;
    public Sprite SkullKnightXoer;
    public Sprite BlackCat;
    public Sprite Pumpkin;
    public Sprite PumpkinGentleman;
    public Sprite Pumpkinmini;
    public Sprite SteinMonster;
    public Sprite UltraStein;
    public Sprite CarnivorousPlant;
    public Sprite DryadsArcher;
    public Sprite DryadsMage;
    public Sprite DryadsWarrior;
    public Sprite Hydra;
    public Sprite Yggdrasil;
    public Sprite ToxicRoot;
    public Sprite UndeadClawKnight;
    public Sprite UndeadGigaraven;
    public Sprite UndeadSkullTree;
    public Sprite UndeadWalker;
    public Sprite UndeadWarrior;
    public Sprite UndeadWolf;
    public Sprite Banshee;
    public Sprite DarkAxeWarrior;
    public Sprite DarkHealer;
    public Sprite DarknessDullahan;
    public Sprite DarknessReaper;
    public Sprite ShadowKnight;
    public Sprite DarkMonk;
    public Sprite DragonEmperorZalaras;
    public Sprite GhostKnight;
    public Sprite Greatwitch;
    public Sprite Succubus;
    public Sprite Vampire;
    public Sprite Witch;
    public Sprite EldritchEyes;
    public Sprite EldritchslimetypeA;
    public Sprite EldritchslimetypeB;
    public Sprite EldritchslimetypeC;
    public Sprite EldritchslimetypeD;
    public Sprite EldritchslimetypeF;
    public Sprite KoboldPaladin;
    public Sprite KoboldsDaggerKobold;
    public Sprite KoboltRogue;
    public Sprite koboltultraknight;
    public Sprite MageKobold;
    public Sprite SpearKobold;
    public Sprite KnightAxeElite;
    public Sprite KnightBlunderbussElite;
    public Sprite KnightSpearElite;
    public Sprite RedGuardknuckles;
    public Sprite RedGuardsniper;
    public Sprite RedGuardwarrior;
    public Sprite BookMaster;
    public Sprite Innova;
    public Sprite Novus;
    public Sprite RedGuardKnight;
    public Sprite RedguardReaper;
    public Sprite AbominationHound;
    public Sprite AbominationTyrant;
    public Sprite AbominationsScout;
    public Sprite Cultist;
    public Sprite GodYoggoth;
    public Sprite KingYoggoth;
    public Sprite QueenYoggoth;
    public Sprite AbominationGazer;

    public GameObject petimg;

    void Start()
    {
        DB();
        GetUserStats();
    }
    public void Refreshlist()
    {
        DB();
        GetUserStats();
        //Getdata();
    }
    public void showact1()
    {
        Panelact1.SetActive(true);
        Panelact2.SetActive(false);
        Panelact3.SetActive(false);
        Panelact4.SetActive(false);
        Panelact5.SetActive(false);
    }
    public void showact2()
    {
        Panelact1.SetActive(false);
        Panelact2.SetActive(true);
        Panelact3.SetActive(false);
        Panelact4.SetActive(false);
        Panelact5.SetActive(false);
    }
    public void showact3()
    {
        Panelact1.SetActive(false);
        Panelact2.SetActive(false);
        Panelact3.SetActive(true);
        Panelact4.SetActive(false);
        Panelact5.SetActive(false);
    }
    public void showact4()
    {
        Panelact1.SetActive(false);
        Panelact2.SetActive(false);
        Panelact3.SetActive(false);
        Panelact4.SetActive(true);
        Panelact5.SetActive(false);
    }
    public void showact5()
    {
        Panelact1.SetActive(false);
        Panelact2.SetActive(false);
        Panelact3.SetActive(false);
        Panelact4.SetActive(false);
        Panelact5.SetActive(true);
    }
    void DB()
    {
        FirebaseApp.DefaultInstance.SetEditorDatabaseUrl("https://pvpet-f0b05.firebaseio.com/Players/Pa5UU16uCzt6X1E1DJ6a");
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }
    void GetUserStats()
    {
        string uid;
        //OBTIENE ID DE USUARIO
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        textuserid.text = GameController.userid;
        uid = textuserid.text.ToString();
        uid = "8xLUp3Df6tW4wOOQOICsmmUswiq1";
        //OBTIENE LABES PARA PANTALLA
        Text textUsername = GameObject.Find("Canvas/lbl_username").GetComponent<Text>();
        Text textHP = GameObject.Find("Canvas/lbl_pv").GetComponent<Text>();
        Text textAgility = GameObject.Find("Canvas/lbl_ag").GetComponent<Text>();
        Text textSpeed = GameObject.Find("Canvas/lbl_sp").GetComponent<Text>();
        Text textStrength = GameObject.Find("Canvas/lbl_str").GetComponent<Text>();
        Text textArmorv = GameObject.Find("Canvas/lbl_arm").GetComponent<Text>();

        //reference.Child("users").Child(uid).Child("PVE").Child("available").SetValueAsync("1000");
        //CONECTA CON BASE
        FirebaseDatabase.DefaultInstance.GetReference("users").Child(uid).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("DataManager: read database is faulted with error: " + task.Exception.ToString());
                return;
            }
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Dictionary<string, System.Object> attributes = (Dictionary<string, System.Object>)snapshot.Value;
                if (snapshot.Exists)
                {
                    //GUARDA TODOS LOS DATOS DE USUARIO, LLENA LAS VARIABLES DE USUARIO Y CAMBIA LOS LABELS PARA LA PANTALLA
                    Dictionary<string, System.Object> PVE = (Dictionary<string, System.Object>)attributes["PVE"];
                    hpuser = int.Parse(attributes["HP"].ToString());
                    struser = int.Parse(attributes["Strength"].ToString());
                    aguser = int.Parse(attributes["Agility"].ToString());
                    speuser = int.Parse(attributes["Speed"].ToString());
                    armuser = int.Parse(attributes["Armorv"].ToString());
                    expuser = int.Parse(attributes["Armorv"].ToString());
                    available = int.Parse(PVE["available"].ToString());
                    
                    textUsername.text = attributes["username"].ToString();
                    textHP.text = "HP:" + attributes["HP"].ToString();
                    textAgility.text = "AGY:" + attributes["Agility"].ToString();
                    textSpeed.text = "SPE:" + attributes["Speed"].ToString();
                    textStrength.text = "STR:" + attributes["Strength"].ToString();
                    textArmorv.text = "ARM:" + attributes["Armorv"].ToString();
                }
                else
                {
                    Debug.LogError("DataManager: Database for the user not available.");
                }
            }
        });
    }
    public void preparativosmision()
    {
        Panelpreparativos.SetActive(true);
        Text Act = GameObject.Find("Canvas/pnl_preparativos/lbl_act_v").GetComponent<Text>();
        Text Cap = GameObject.Find("Canvas/pnl_preparativos/lbl_cap_v").GetComponent<Text>();
        Text Tit = GameObject.Find("Canvas/pnl_preparativos/lbl_tit_v").GetComponent<Text>();
        Act.text = Acto.ToString();
        Cap.text = Capitulo.ToString();
        Tit.text = titulo;
       
    }
    public void gomision()
    {

        Text Act = GameObject.Find("Canvas/pnl_preparativos/lbl_act_v").GetComponent<Text>();
        Text Cap = GameObject.Find("Canvas/pnl_preparativos/lbl_cap_v").GetComponent<Text>();
        Text Tit = GameObject.Find("Canvas/pnl_preparativos/lbl_tit_v").GetComponent<Text>();
        int Mcla = int.Parse(Act.text);
        int Mcap = int.Parse(Cap.text);
        string clase = "Clase" + Mcla;
        string mision = "Mision" + Mcap;
        int VO = 0;
        int LE = 0;
        int MUL = 0;
        int ddDificultad = dropdownMenu.GetComponent<Dropdown>().value;
        List<Dropdown.OptionData> menuDificultad = dropdownMenu.GetComponent<Dropdown>().options;
        string Diff = menuDificultad[ddDificultad].text;
        DB();
        GetUserStats();
        available = available - 1;
        string uid;
        Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
        textuserid.text = GameController.userid;
        uid = textuserid.text.ToString();
        uid = "8xLUp3Df6tW4wOOQOICsmmUswiq1";
        reference.Child("users").Child(uid).Child("PVE").Child("available").SetValueAsync(available);
        Debug.Log(Diff);
        switch (Diff)
        {
            case "Facil":
                LE = 1;VO = 10; MUL = 1;
                break;
            case "Normal":
                LE = 10; VO = 20; MUL = 2;
                break;
            case "Dificil":
                LE = 20; VO = 30; MUL = 3;
                break;
            case "Extremo":
                LE = 30; VO = 40; MUL = 4;
                break;
            case "Tormento1":
                LE = 40; VO = 60; MUL = 5;
                break;
            case "Tormento2":
                LE = 60; VO = 90; MUL = 6;
                break;
            case "Tormento3":
                LE = 90; VO = 150; MUL = 7;
                break;
            case "PetHunter":
                LE = 150; VO = 300; MUL = 8;
                break;
        };

        VO = (VO + Mcap) * MUL;
        LE = (LE + Mcap) * MUL;
        FirebaseDatabase.DefaultInstance.GetReference("PvEPets").Child(clase).Child(mision).GetValueAsync().ContinueWith(task =>
        {
            if (task.IsFaulted)
            {
                Debug.LogError("DataManager: read database is faulted with error: " + task.Exception.ToString());
                return;
            }
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                Dictionary<string, System.Object> pets = (Dictionary<string, System.Object>)snapshot.Value;
                if (snapshot.Exists)
                {
                    List<Itemc> items = new List<Itemc>();
                    foreach (KeyValuePair<string, System.Object> pair in pets)
                    {
                        Dictionary<string, System.Object> pet = (Dictionary<string, System.Object>)pets[pair.Key];
                        items.Add(new Itemc() { name = pet["Nombre"].ToString(), chance = int.Parse(pet[Diff].ToString()) });
                    }
                    
                    Itemc Petichooseyou = ProportionalWheelSelection.SelectItem(items);
                    
                    int pethp = UnityEngine.Random.Range(LE, VO);
                    int petstr = UnityEngine.Random.Range(LE, VO);
                    int petagy = UnityEngine.Random.Range(LE, VO);
                    int petspe = UnityEngine.Random.Range(LE, VO);
                    int petarm = UnityEngine.Random.Range(LE, VO);

                    Debug.Log(Petichooseyou.name);
                    Debug.Log(Petichooseyou.chance);
                    Debug.Log(pethp);
                    Debug.Log(petstr);
                    Debug.Log(petagy);
                    Debug.Log(petspe);
                    Debug.Log(petarm);

                    Panelpreparativos.SetActive(false);
                    Panelmision.SetActive(true);

                    switch (Petichooseyou.name)
                    {
                        case "Bull": petimg.GetComponent<Image>().sprite = Bull; break;
                        case "Lunar Butterfly": petimg.GetComponent<Image>().sprite = LunarButterfly; break;
                        case "Mantis": petimg.GetComponent<Image>().sprite = Mantis; break;
                        case "Roach": petimg.GetComponent<Image>().sprite = Roach; break;
                        case "Scarab": petimg.GetComponent<Image>().sprite = Scarab; break;
                        case "Tick": petimg.GetComponent<Image>().sprite = Tick; break;
                        case "Caterpillar": petimg.GetComponent<Image>().sprite = Caterpillar; break;
                        case "Giant Bug Centipede": petimg.GetComponent<Image>().sprite = GiantBugCentipede; break;
                        case "Giant Bug Death Worm": petimg.GetComponent<Image>().sprite = GiantBugDeathWorm; break;
                        case "Insects Dragon": petimg.GetComponent<Image>().sprite = InsectsDragon; break;
                        case "Red Ant Knight": petimg.GetComponent<Image>().sprite = RedAntKnight; break;
                        case "Waterstrider": petimg.GetComponent<Image>().sprite = Waterstrider; break;
                        case "Black Ant Archer": petimg.GetComponent<Image>().sprite = BlackAntArcher; break;
                        case "Black Ant Berserker": petimg.GetComponent<Image>().sprite = BlackAntBerserker; break;
                        case "Black Ant Knight": petimg.GetComponent<Image>().sprite = BlackAntKnight; break;
                        case "Black Ant Mage": petimg.GetComponent<Image>().sprite = BlackAntMage; break;
                        case "Black Ant Protector": petimg.GetComponent<Image>().sprite = BlackAntProtector; break;
                        case "Golem": petimg.GetComponent<Image>().sprite = Golem; break;
                        case "Death Worm": petimg.GetComponent<Image>().sprite = DeathWorm; break;
                        case "Giant Bug Hercules": petimg.GetComponent<Image>().sprite = GiantBugHercules; break;
                        case "Hell Mantis": petimg.GetComponent<Image>().sprite = HellMantis; break;
                        case "Swarm": petimg.GetComponent<Image>().sprite = Swarm; break;
                        case "Titan Tellia": petimg.GetComponent<Image>().sprite = TitanTellia; break;
                        case "Tridentpupa": petimg.GetComponent<Image>().sprite = Tridentpupa; break;
                        case "Dryad Mini": petimg.GetComponent<Image>().sprite = DryadMini; break;
                        case "Earth Dragon": petimg.GetComponent<Image>().sprite = EarthDragon; break;
                        case "Forest Spider": petimg.GetComponent<Image>().sprite = ForestSpider; break;
                        case "Imperial Widow": petimg.GetComponent<Image>().sprite = ImperialWidow; break;
                        case "Six-Wing Fairy": petimg.GetComponent<Image>().sprite = SixWingFairy; break;
                        case "Feral Kitsune": petimg.GetComponent<Image>().sprite = FeralKitsune; break;
                        case "Rabbit Warriors Archer": petimg.GetComponent<Image>().sprite = RabbitWarriorsArcher; break;
                        case "Rabbit Warriors Bandit": petimg.GetComponent<Image>().sprite = RabbitWarriorsBandit; break;
                        case "Rabbit Warriors Knight": petimg.GetComponent<Image>().sprite = RabbitWarriorsKnight; break;
                        case "Seven Sins Greed": petimg.GetComponent<Image>().sprite = SevenSinsGreed; break;
                        case "Wind Snake": petimg.GetComponent<Image>().sprite = WindSnake; break;
                        case "Deer": petimg.GetComponent<Image>().sprite = Deer; break;
                        case "Elf_Assasin": petimg.GetComponent<Image>().sprite = Elf_Assasin; break;
                        case "Elves Rapier": petimg.GetComponent<Image>().sprite = ElvesRapier; break;
                        case "Elves Rogue Elf": petimg.GetComponent<Image>().sprite = ElvesRogueElf; break;
                        case "Elves Spellcaster": petimg.GetComponent<Image>().sprite = ElvesSpellcaster; break;
                        case "Fairy Filia": petimg.GetComponent<Image>().sprite = FairyFilia; break;
                        case "Arcane Golem": petimg.GetComponent<Image>().sprite = ArcaneGolem; break;
                        case "Gemstone Fire": petimg.GetComponent<Image>().sprite = GemstoneFire; break;
                        case "Gemstone Thunder": petimg.GetComponent<Image>().sprite = GemstoneThunder; break;
                        case "Gemstone Water": petimg.GetComponent<Image>().sprite = GemstoneWater; break;
                        case "Gemstone Wind": petimg.GetComponent<Image>().sprite = GemstoneWind; break;
                        case "Orb Fire": petimg.GetComponent<Image>().sprite = OrbFire; break;
                        case "Orb Frost": petimg.GetComponent<Image>().sprite = OrbFrost; break;
                        case "Orb Thunder": petimg.GetComponent<Image>().sprite = OrbThunder; break;
                        case "Orb Wind": petimg.GetComponent<Image>().sprite = OrbWind; break;
                        case "Elemental Earth Spirit Tellia": petimg.GetComponent<Image>().sprite = ElementalEarthSpiritTellia; break;
                        case "Elemental Goddess Airi": petimg.GetComponent<Image>().sprite = ElementalGoddessAiri; break;
                        case "Elemental Goddess Flora": petimg.GetComponent<Image>().sprite = ElementalGoddessFlora; break;
                        case "Elemental Goddess imp": petimg.GetComponent<Image>().sprite = ElementalGoddessimp; break;
                        case "Elemental Goddess Yukia": petimg.GetComponent<Image>().sprite = ElementalGoddessYukia; break;
                        case "Elemental Ice Spirit Helida": petimg.GetComponent<Image>().sprite = ElementalIceSpiritHelida; break;
                        case "Elemental Spirit Fire Blazia": petimg.GetComponent<Image>().sprite = ElementalSpiritFireBlazia; break;
                        case "Elemental Wind Spirit Tempestia": petimg.GetComponent<Image>().sprite = ElementalWindSpiritTempestia; break;
                        case "Dragon King Blue": petimg.GetComponent<Image>().sprite = DragonKingBlue; break;
                        case "Dragon King Brown": petimg.GetComponent<Image>().sprite = DragonKingBrown; break;
                        case "Dragon King Green": petimg.GetComponent<Image>().sprite = DragonKingGreen; break;
                        case "Dragon King Red": petimg.GetComponent<Image>().sprite = DragonKingRed; break;
                        case "Egypt Archer": petimg.GetComponent<Image>().sprite = EgyptArcher; break;
                        case "Egypt Axe": petimg.GetComponent<Image>().sprite = EgyptAxe; break;
                        case "Egypt Chariot": petimg.GetComponent<Image>().sprite = EgyptChariot; break;
                        case "Egypt Knight": petimg.GetComponent<Image>().sprite = EgyptKnight; break;
                        case "Egypt Mage": petimg.GetComponent<Image>().sprite = EgyptMage; break;
                        case "Hieracosphinx": petimg.GetComponent<Image>().sprite = Hieracosphinx; break;
                        case "Cobra": petimg.GetComponent<Image>().sprite = Cobra; break;
                        case "Crocodile": petimg.GetComponent<Image>().sprite = Crocodile; break;
                        case "Mummy": petimg.GetComponent<Image>().sprite = Mummy; break;
                        case "Sphinx": petimg.GetComponent<Image>().sprite = Sphinx; break;
                        case "Pirate Bandit": petimg.GetComponent<Image>().sprite = PirateBandit; break;
                        case "Pirate Captain": petimg.GetComponent<Image>().sprite = PirateCaptain; break;
                        case "Pirate Magic Scimitar": petimg.GetComponent<Image>().sprite = PirateMagicScimitar; break;
                        case "Pirate Monkey": petimg.GetComponent<Image>().sprite = PirateMonkey; break;
                        case "Pirate Parrot": petimg.GetComponent<Image>().sprite = PirateParrot; break;
                        case "Pirate Skeleton": petimg.GetComponent<Image>().sprite = PirateSkeleton; break;
                        case "Turtle Golem": petimg.GetComponent<Image>().sprite = TurtleGolem; break;
                        case "Mermaid": petimg.GetComponent<Image>().sprite = Mermaid; break;
                        case "Mermaid Warrior Arliette": petimg.GetComponent<Image>().sprite = MermaidWarriorArliette; break;
                        case "Mermaid Warrior Sasha": petimg.GetComponent<Image>().sprite = MermaidWarriorSasha; break;
                        case "Mermaid Warrior Sion": petimg.GetComponent<Image>().sprite = MermaidWarriorSion; break;
                        case "Octopus": petimg.GetComponent<Image>().sprite = Octopus; break;
                        case "Piranos": petimg.GetComponent<Image>().sprite = Piranos; break;
                        case "Shark": petimg.GetComponent<Image>().sprite = Shark; break;
                        case "Titan Aquos": petimg.GetComponent<Image>().sprite = TitanAquos; break;
                        case "Skeleton Archer": petimg.GetComponent<Image>().sprite = SkeletonArcher; break;
                        case "Skeleton Dragon": petimg.GetComponent<Image>().sprite = SkeletonDragon; break;
                        case "Skeleton Knight": petimg.GetComponent<Image>().sprite = SkeletonKnight; break;
                        case "Skeleton Knight Baron": petimg.GetComponent<Image>().sprite = SkeletonKnightBaron; break;
                        case "Skeleton Mage": petimg.GetComponent<Image>().sprite = SkeletonMage; break;
                        case "Skull Knight Xoer": petimg.GetComponent<Image>().sprite = SkullKnightXoer; break;
                        case "Black Cat": petimg.GetComponent<Image>().sprite = BlackCat; break;
                        case "Pumpkin": petimg.GetComponent<Image>().sprite = Pumpkin; break;
                        case "Pumpkin Gentleman": petimg.GetComponent<Image>().sprite = PumpkinGentleman; break;
                        case "Pumpkin mini": petimg.GetComponent<Image>().sprite = Pumpkinmini; break;
                        case "Stein Monster": petimg.GetComponent<Image>().sprite = SteinMonster; break;
                        case "Ultra Stein": petimg.GetComponent<Image>().sprite = UltraStein; break;
                        case "Carnivorous Plant": petimg.GetComponent<Image>().sprite = CarnivorousPlant; break;
                        case "Dryads Archer": petimg.GetComponent<Image>().sprite = DryadsArcher; break;
                        case "Dryads Mage": petimg.GetComponent<Image>().sprite = DryadsMage; break;
                        case "Dryads Warrior": petimg.GetComponent<Image>().sprite = DryadsWarrior; break;
                        case "Hydra": petimg.GetComponent<Image>().sprite = Hydra; break;
                        case "Yggdrasil": petimg.GetComponent<Image>().sprite = Yggdrasil; break;
                        case "Toxic Root": petimg.GetComponent<Image>().sprite = ToxicRoot; break;
                        case "Undead Claw Knight": petimg.GetComponent<Image>().sprite = UndeadClawKnight; break;
                        case "Undead Gigaraven": petimg.GetComponent<Image>().sprite = UndeadGigaraven; break;
                        case "Undead Skull Tree": petimg.GetComponent<Image>().sprite = UndeadSkullTree; break;
                        case "Undead Walker": petimg.GetComponent<Image>().sprite = UndeadWalker; break;
                        case "Undead Warrior": petimg.GetComponent<Image>().sprite = UndeadWarrior; break;
                        case "Undead Wolf": petimg.GetComponent<Image>().sprite = UndeadWolf; break;
                        case "Banshee": petimg.GetComponent<Image>().sprite = Banshee; break;
                        case "Dark Axe Warrior": petimg.GetComponent<Image>().sprite = DarkAxeWarrior; break;
                        case "Dark Healer": petimg.GetComponent<Image>().sprite = DarkHealer; break;
                        case "Darkness Dullahan": petimg.GetComponent<Image>().sprite = DarknessDullahan; break;
                        case "Darkness Reaper": petimg.GetComponent<Image>().sprite = DarknessReaper; break;
                        case "Shadow Knight": petimg.GetComponent<Image>().sprite = ShadowKnight; break;
                        case "Dark Monk": petimg.GetComponent<Image>().sprite = DarkMonk; break;
                        case "Dragon Emperor Zalaras": petimg.GetComponent<Image>().sprite = DragonEmperorZalaras; break;
                        case "Ghost Knight": petimg.GetComponent<Image>().sprite = GhostKnight; break;
                        case "Great witch": petimg.GetComponent<Image>().sprite = Greatwitch; break;
                        case "Succubus": petimg.GetComponent<Image>().sprite = Succubus; break;
                        case "Vampire": petimg.GetComponent<Image>().sprite = Vampire; break;
                        case "Witch": petimg.GetComponent<Image>().sprite = Witch; break;
                        case "Eldritch Eyes": petimg.GetComponent<Image>().sprite = EldritchEyes; break;
                        case "Eldritch slime type A": petimg.GetComponent<Image>().sprite = EldritchslimetypeA; break;
                        case "Eldritch slime type B": petimg.GetComponent<Image>().sprite = EldritchslimetypeB; break;
                        case "Eldritch slime type C": petimg.GetComponent<Image>().sprite = EldritchslimetypeC; break;
                        case "Eldritch slime type D": petimg.GetComponent<Image>().sprite = EldritchslimetypeD; break;
                        case "Eldritch slime type F": petimg.GetComponent<Image>().sprite = EldritchslimetypeF; break;
                        case "Kobold Paladin": petimg.GetComponent<Image>().sprite = KoboldPaladin; break;
                        case "Kobolds Dagger Kobold": petimg.GetComponent<Image>().sprite = KoboldsDaggerKobold; break;
                        case "Kobolt Rogue": petimg.GetComponent<Image>().sprite = KoboltRogue; break;
                        case "kobolt ultra knight": petimg.GetComponent<Image>().sprite = koboltultraknight; break;
                        case "Mage Kobold": petimg.GetComponent<Image>().sprite = MageKobold; break;
                        case "Spear Kobold": petimg.GetComponent<Image>().sprite = SpearKobold; break;
                        case "Knight Axe Elite": petimg.GetComponent<Image>().sprite = KnightAxeElite; break;
                        case "Knight Blunderbuss Elite": petimg.GetComponent<Image>().sprite = KnightBlunderbussElite; break;
                        case "Knight Spear Elite": petimg.GetComponent<Image>().sprite = KnightSpearElite; break;
                        case "Red Guard knuckles": petimg.GetComponent<Image>().sprite = RedGuardknuckles; break;
                        case "Red Guard sniper": petimg.GetComponent<Image>().sprite = RedGuardsniper; break;
                        case "Red Guard warrior": petimg.GetComponent<Image>().sprite = RedGuardwarrior; break;
                        case "Book Master": petimg.GetComponent<Image>().sprite = BookMaster; break;
                        case "Innova": petimg.GetComponent<Image>().sprite = Innova; break;
                        case "Novus": petimg.GetComponent<Image>().sprite = Novus; break;
                        case "Red Guard Knight": petimg.GetComponent<Image>().sprite = RedGuardKnight; break;
                        case "Red guard Reaper": petimg.GetComponent<Image>().sprite = RedguardReaper; break;
                        case "Abomination Hound": petimg.GetComponent<Image>().sprite = AbominationHound; break;
                        case "Abomination Tyrant": petimg.GetComponent<Image>().sprite = AbominationTyrant; break;
                        case "Abominations Scout": petimg.GetComponent<Image>().sprite = AbominationsScout; break;
                        case "Cultist": petimg.GetComponent<Image>().sprite = Cultist; break;
                        case "God Yoggoth": petimg.GetComponent<Image>().sprite = GodYoggoth; break;
                        case "King Yoggoth": petimg.GetComponent<Image>().sprite = KingYoggoth; break;
                        case "Queen Yoggoth": petimg.GetComponent<Image>().sprite = QueenYoggoth; break;
                        case "Abomination Gazer": petimg.GetComponent<Image>().sprite = AbominationGazer; break;
                    };
                }
            }
        });

    }
    public void exitmision()
    {
        Panelmision.SetActive(false);
    }


    void Getdata()
    {
        Firebase.Database.FirebaseDatabase dbInstance = Firebase.Database.FirebaseDatabase.DefaultInstance;
        dbInstance.GetReference("users").GetValueAsync().ContinueWith(task =>
        {
            if (task.IsCompleted)
            {
                DataSnapshot snapshot = task.Result;
                foreach (DataSnapshot user in snapshot.Children)
                {
                    IDictionary dictUser = (IDictionary)user.Value;
                    Debug.Log(dictUser["username"].ToString());
                }
            }
        });
    }

    public void Cancelmision()
    {
        Panelpreparativos.SetActive(false);
    }
}


public class Itemc
{
    public string name; // not only string, any type of data
    public int chance;  // chance of getting this Item
}

public class ProportionalWheelSelection
{

    public static Random2 rnd = new Random2();
    public static Itemc SelectItem(List<Itemc> items)
    {
        // Calculate the summa of all portions.
        int poolSize = 0;
        for (int i = 0; i < items.Count; i++)
        {
            poolSize += items[i].chance;
        }

        // Get a random integer from 0 to PoolSize.
        int randomNumber = rnd.Next(0, poolSize) + 1;

        // Detect the item, which corresponds to current random number.
        int accumulatedProbability = 0;
        for (int i = 0; i < items.Count; i++)
        {
            accumulatedProbability += items[i].chance;
            if (randomNumber <= accumulatedProbability)
                return items[i];
        }
        return null;    // this code will never come while you use this programm right :)
    }
}
