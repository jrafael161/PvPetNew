using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToPvP : MonoBehaviour
{
    public void GoToPvPSc()
    {
        SceneManager.LoadScene("PvPScreen");
    }
}
