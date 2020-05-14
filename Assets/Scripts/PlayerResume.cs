using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerResume : MonoBehaviour
{
    // Start is called before the first frame update
    PlayerData player;
    GameObject Stats;
    GameObject Currency;
    GameObject PlayerPortrait;
    public GameObject EquipedItem;
    public GameObject OwnedPet;

    void Start()
    {
        InitializePlayerData();
    }

    public void InitializePlayerData(PlayerData player = null)//Se muestra la informacion del jugador segun lo requerido
    {
        if (player == null)
        {
            player = GlobalControl.Instance.playeProfile;
        }
        bool leveled = false;
        Slider sli;
        Text hp_Text;
        GameObject hpStat, currency;
        GameObject[] stats = new GameObject[3];
        Text[] PlayerCoins = new Text[3];
        stats[0] = GameObject.Find("StrengthStat");
        stats[1] = GameObject.Find("AgilityStat");
        stats[2] = GameObject.Find("SpeedStat");
        hpStat = GameObject.Find("HPStat");
        currency = GameObject.Find("Currency");
        PlayerCoins = currency.GetComponentsInChildren<Text>();
        hp_Text = hpStat.GetComponentInChildren<Text>();
        hp_Text.text = "HP:" + player.HP.ToString();
        for (int i = 0; i <= 2; i++)
        {
            sli = stats[i].GetComponentInChildren<Slider>();
            switch (i)
            {
                case 0:
                    sli.value = player.Strength;
                    break;
                case 1:
                    sli.value = player.Agility;
                    break;
                case 2:
                    sli.value = player.Speed;
                    break;
                default:
                    break;
            }
        }
        for (int i = 0; i <= 2; i++)
        {
            switch (i)
            {
                case 0:
                    PlayerCoins[i].text = player.PetCoin.ToString();
                    break;
                case 1:
                    PlayerCoins[i].text = player.PvPCoin.ToString();
                    break;
                case 2:
                    PlayerCoins[i].text = player.PremiumCoin.ToString();
                    break;
                default:
                    break;
            }
        }
        GameObject EquipedItemAux;
        Image img;
        foreach (Item item in player.EquipedItems)
        {
            EquipedItemAux = Instantiate(EquipedItem) as GameObject;
            //EquipedItemAux.SetActive(true);
            EquipedItemAux.transform.SetParent(EquipedItem.transform.parent, false);
            img = EquipedItemAux.GetComponentInChildren<Image>();
            img.sprite = item.icon;
        }
        EquipedItem.SetActive(false);

        GameObject OwnedPetAux;
        Text[] Texto;
        foreach (Pet pet in player.OwnedPets)
        {
            Pet aux;
            OwnedPetAux = Instantiate(OwnedPet) as GameObject;
            //OwnedPetAux.SetActive(true);
            OwnedPetAux.transform.SetParent(OwnedPet.transform.parent, false);
            Texto = OwnedPetAux.GetComponentsInChildren<Text>();
            img = OwnedPetAux.GetComponentInChildren<Image>();
            Texto[0].text = pet.PetName.ToString();
            img.sprite = PetDBManager.Instance.PetDB.Find(x => x.PetName == pet.PetName).PetSprite;
            Texto[1].text = pet.Level.ToString();
            Texto[2].text = pet.HP.ToString();
            Texto[3].text = pet.Strength.ToString();
            Texto[4].text = pet.Speed.ToString();
            Texto[5].text = pet.Agility.ToString();
        }
        OwnedPet.SetActive(false);

        if (player.LevelUpPoints >= 1)
        {
            foreach (GameObject stat in stats)
            {
                Button[] levelControlers = stat.GetComponentsInChildren<Button>(true);
                foreach (Button but in levelControlers)
                {
                    but.gameObject.SetActive(true);
                }
            }
            leveled = true;
        }
        else if (player == null && player.LevelUpPoints == 0)
        {
            if (leveled)
            {
                foreach (GameObject stat in stats)
                {
                    Button[] levelControlers = stat.GetComponentsInChildren<Button>(true);
                    foreach (Button but in levelControlers)
                    {
                        but.gameObject.SetActive(false);
                    }
                }
                leveled = false;
            }
        }
    }
}
