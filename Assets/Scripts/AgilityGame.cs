using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgilityGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform SpawnPoint;
    public GameObject player;
    public Vector2 MousePos;

    void Start()
    {
        SpawnPlayer(SpawnPoint);
        StartCoroutine("Movement");
    }

    // Update is called once per frame
    void Update()
    {        
    }

    public void SpawnPlayer(Transform spawnpoint)
    {
        player.transform.position = spawnpoint.transform.position;
        Eframes();
    }

    public void Eframes()
    {
        //Darle Invulnerabilidad al jugador durante 3 o 5 seg cuando hace spawn
    }

    void MovePlayer()
    {
        player.transform.Translate(new Vector3(MousePos.x*75, MousePos.y*75, 0));
        if (player.transform.position.x > Screen.width)
        {
            player.transform.position = new Vector3(Screen.width - 146, player.transform.position.y,0);
        }
        else if (player.transform.position.x < 0)
        {
            player.transform.position = new Vector3(0 + 146, player.transform.position.y, 0);
        }
        if (player.transform.position.y > Screen.height)
        {
            player.transform.position = new Vector3(player.transform.position.x, Screen.height - 146 , 0);
        }
        else if (player.transform.position.y < 0)
        {
            player.transform.position = new Vector3(player.transform.position.x, 0 + 146, 0);
        }
    }

    private IEnumerator Movement()
    {
        while (true)
        {
            MousePos = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
                //Debug.Log("Pos X: " + Input.GetAxis("Mouse X") + "Pos en Y: " + Input.GetAxis("Mouse Y"));
            MovePlayer();
            //yield return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
            yield return null;
        }
    }
}
