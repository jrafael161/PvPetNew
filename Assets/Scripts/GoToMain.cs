using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMain : MonoBehaviour
{
    public void GoToMainMenu()
    {
        Debug.Log("intento cargar la escena");
        SceneManager.LoadScene("MainMenuScreen");
        Debug.Log("cargo la escena?");
    }
}
