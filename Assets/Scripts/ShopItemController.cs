using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ShopItemController : MonoBehaviour
{
    Button canvasBack;
    GameObject aux;

    public void PurchasePrompt(GameObject shopItem)
    {
        Text[] TextoOriginal;
        Text[] TextoPreview;
        Image[] SpriteOriginal;
        Image[] SpritePreview;

        shopItem.SetActive(true);
        TextoOriginal = aux.GetComponentsInChildren<Text>();
        TextoPreview = shopItem.GetComponentsInChildren<Text>();
        SpriteOriginal = aux.GetComponentsInChildren<Image>();
        SpritePreview = shopItem.GetComponentsInChildren<Image>();
        for (int i = 0; i < TextoOriginal.Length; i++)
        {
            TextoPreview[i].text = TextoOriginal[i].text;
        }
        SpritePreview[2].sprite = SpriteOriginal[2].sprite;
    }

    public void GetShopItem(GameObject shopItem)
    {
        aux = shopItem;
    }

    public void DeactivatePreview()
    {
        GameObject preview = GameObject.Find("ShopItemPreview");
        preview.SetActive(false);
    }

    public void ActivateExitPreview(GameObject exitButton)
    {
        exitButton.SetActive(true);
    }

    public void DeactivateExit(GameObject exitButton)
    {
        exitButton.SetActive(false);
    }

}
