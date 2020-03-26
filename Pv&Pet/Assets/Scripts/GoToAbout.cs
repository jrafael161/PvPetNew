using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToAbout : MonoBehaviour
{
    public void GoToAbt()
    {
        SceneManager.LoadScene("AboutScreen");
    }
}
