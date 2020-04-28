using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToConf : MonoBehaviour
{
    public void GoToConfigurations()
    {
        SceneManager.LoadScene("ConfigurationScreen");
    }
}
