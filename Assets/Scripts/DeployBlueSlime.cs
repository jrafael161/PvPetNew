using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DeployBlueSlime : MonoBehaviour
{
    public GameObject BlueSplimePrefab;
    public GameObject RedSplimePrefab;
    public GameObject PurpleSplimePrefab;
    public GameObject Pnlstart;
    public float Bluerespawn = 3.0f;
    public float Redrespawn = 15.0f;
    public float Purplerespawn = 30.0f;
    public bool startBool = true;
    private Vector2 screenBounds;

    void Start()
    {
    }

    void Update()
    {
        Text Lives = GameObject.Find("Canvas/bg_score/Lbl_v").GetComponent<Text>();
        int Livesint = int.Parse(Lives.text);
        if (Livesint <= 0)
        {
            SceneManager.LoadScene("Minigame_str");
        }

        Text Nivel = GameObject.Find("Canvas/bg_score/Lbl_n").GetComponent<Text>();
        int NivelInt = int.Parse(Nivel.text);
        switch (NivelInt)
        {
            case 1:
                Bluerespawn = 3.0f;
                break;
            case 2:
                Bluerespawn = 2.5f;
                break;
            case 3:
                Bluerespawn = 1.0f;
                break;
            case 4:
                Redrespawn = 8.0f;
                break;
            case 5:
                Redrespawn = 6.0f;
                break;
            case 6:
                Redrespawn = 4.0f;
                Purplerespawn = 8.0f;
                break;
            case 7:
                Redrespawn = 3.0f;
                Purplerespawn = 7.0f;
                break;
            case 8:
                Redrespawn = 2.0f;
                Purplerespawn = 5.0f;
                break;
            case 9:
                Redrespawn = 1.5f;
                Purplerespawn = 3.0f;
                break;
            case 10:
                Redrespawn = 1.0f;
                Purplerespawn = 2.0f;
                break;
            case 11:
                Redrespawn = 0.5f;
                Purplerespawn = 0.5f;
                break;
        };
    }
    public void Startgame()
    {
        startBool = true;
        Pnlstart.SetActive(false);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        StartCoroutine(BlueSlimeWave());
        StartCoroutine(RedSlimeWave());
        StartCoroutine(PurpleSlimeWave());
    }
    private void SpawnBlueSlimeEnemy()
    {
        GameObject a = Instantiate(BlueSplimePrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
        a.transform.position = new Vector2(screenBounds.x, Random.Range(-screenBounds.y, screenBounds.y));
    }
    private void SpawnRedSlimeEnemy()
    {
        GameObject a = Instantiate(RedSplimePrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
        a.transform.position = new Vector2(screenBounds.x, Random.Range(-screenBounds.y, screenBounds.y));
    }
    private void SpawnPurpleSlimeEnemy()
    {
        GameObject a = Instantiate(PurpleSplimePrefab, Vector3.zero, Quaternion.identity, GameObject.Find("Canvas").transform);
        a.transform.position = new Vector2(screenBounds.x, Random.Range(-screenBounds.y, screenBounds.y));
    }
    IEnumerator BlueSlimeWave()
    {
        while (startBool)
        {
            yield return new WaitForSeconds(Bluerespawn);
            SpawnBlueSlimeEnemy();
        }
    }
    IEnumerator RedSlimeWave()
    {
        while (startBool)
        {
            yield return new WaitForSeconds(Redrespawn);
            SpawnRedSlimeEnemy();
        }
    }
    IEnumerator PurpleSlimeWave()
    {
        while (startBool)
        {
            yield return new WaitForSeconds(Purplerespawn);
            SpawnPurpleSlimeEnemy();
        }
    }
    void DestroyAll(string tag)
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(tag);
        for (int i = 0; i < enemies.Length; i++)
        {
            Destroy(enemies[i]);
        }
    }
}
