using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HomeScript : MonoBehaviour
{
    private void Start()
    {
        if (GameObject.Find("Canvas/Txt_userid"))
        {
            Text textuserid = GameObject.Find("Canvas/Txt_userid").GetComponent<Text>();
            textuserid.text = GameController.userid;
        }
    }
    public void GOHOME()
    {
        SceneManager.LoadScene("01-Main");
    }
    public void GOINV()
    {
        SceneManager.LoadScene("02-INV");
    }
    public void GOPVP()
    {
        SceneManager.LoadScene("03-PVP");
    }
    public void GOPVE()
    {
        SceneManager.LoadScene("04-PVE");
    }
    public void GOSTORE()
    {
        SceneManager.LoadScene("06-SHOP");
    }

    public void GOPETS()
    {
        SceneManager.LoadScene("PetSantuary");
    public void GORANKING()
    {
        SceneManager.LoadScene("06-Ranking");
    }
    public void GOBESTIARY()
    {
        SceneManager.LoadScene("05-Bestiario");
    }

    public void GOPETSMINIGAMES()
    {
        SceneManager.LoadScene("PetMinigames");
    }
}
