using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AgilityGame : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform SpawnPoint;
    public GameObject player;
    public GameObject projectile;
    public GameObject trackProjectile;
    public GameObject obstacle;
    public GameObject explosiveProjectile;
    public GameObject miniProjectile;
    public Vector2 MousePos;
    static float TimeBetweenPSpawn = 10;
    static float TimeBetweenOSpawn = 10;
    private List<GameObject> Projectiles;
    private List<GameObject> TrackProjectiles;
    private List<Vector3> TrackingPos;
    private List<GameObject> Obstacles;
    private List<GameObject> ExplosiveProjectiles;
    private List<GameObject> MiniProjectiles;
    private List<Vector3> TrackingMiniPos;
    public float timeAtStart;
    public float timePassed;

    private Image playerSprite;
    public TMP_Text Level;
    public static int level = 0;
    private static int lives=3;

    bool m_Fading;

    void Start()
    {
        playerSprite = player.GetComponent<Image>();
        Projectiles = new List<GameObject>();
        TrackProjectiles = new List<GameObject>();
        TrackingPos = new List<Vector3>();
        TrackingMiniPos = new List<Vector3>();
        Obstacles = new List<GameObject>();
        ExplosiveProjectiles = new List<GameObject>();
        MiniProjectiles = new List<GameObject>();
        SpawnPlayer(SpawnPoint);
        StartCoroutine("PlayerMovement");
        StartCoroutine("SpawnProjectiles");
        StartCoroutine("MoveProjectile");
        StartCoroutine("SpawnObstacles");
        StartCoroutine("MoveObstacles");
        StartCoroutine("SpawnTrackProjectiles");
        StartCoroutine("MoveTrackProjectile");
        StartCoroutine("SpawnExplosiveProjectiles");
        StartCoroutine("MoveExplosiveProjectiles");        
        StartCoroutine("MoveMiniProjectile");
        timeAtStart = Time.timeSinceLevelLoad;
        timePassed = timeAtStart * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (lives >= 0 && !player.activeInHierarchy)
        {
            lives--;
            SpawnPlayer(SpawnPoint);
        }
        if(Time.timeSinceLevelLoad - timeAtStart > timePassed)
        {
            level++;
            Level.text = "Level:" + level.ToString();
            timePassed = timePassed * 2;
        }
            
    }

    public void SpawnPlayer(Transform spawnpoint)
    {
        Debug.Log("Te quedan:" + lives + " vidas");
        player.SetActive(true);
        player.transform.position = spawnpoint.transform.position;
        Destroy(player.GetComponent<Rigidbody2D>());
        Eframes();
    }

    public void Eframes()
    {
        //player.GetComponent<Rigidbody2D>().gameObject.SetActive(false);
        StartCoroutine("PlayerTwinkle");
    }

    IEnumerator PlayerTwinkle()
    {
        int twinkles = 0;
        bool twinkled = false;
        while (twinkles <5)
        {
            if (!twinkled)
            {
                var tempColor = playerSprite.color;
                tempColor.a = 0;
                playerSprite.color = tempColor;
                twinkled = true;                
                yield return new WaitForSeconds(0.1f);                
            }
            else
            {
                var tempColor = playerSprite.color;
                tempColor.a = 255;
                playerSprite.color = tempColor;
                twinkled = false;
                twinkles++;
                yield return new WaitForSeconds(0.1f);
            }
        }
        if (!player.GetComponent<Rigidbody2D>())
        {
            Rigidbody2D aux = player.AddComponent<Rigidbody2D>();
            aux.bodyType = RigidbodyType2D.Kinematic;
            aux.simulated = true;
            aux.useFullKinematicContacts = true;
            aux.collisionDetectionMode = CollisionDetectionMode2D.Discrete;
            aux.constraints = RigidbodyConstraints2D.FreezeRotation;
        }        
        yield return null;
    }

    void MovePlayer()
    {
        player.transform.Translate(new Vector3(MousePos.x*80, MousePos.y*80, 0));
        if (player.transform.position.x > Screen.width)
        {
            player.transform.position = new Vector3(Screen.width, player.transform.position.y,0);
        }
        else if (player.transform.position.x < 0)
        {
            player.transform.position = new Vector3(0, player.transform.position.y, 0);
        }
        if (player.transform.position.y > Screen.height)
        {
            player.transform.position = new Vector3(player.transform.position.x, Screen.height, 0);
        }
        else if (player.transform.position.y < 0)
        {
            player.transform.position = new Vector3(player.transform.position.x, 0, 0);
        }
    }

    public void ExplodeProjectile(GameObject Projectile)
    {
        SpawnMiniProjectiles(Projectile.transform);
        Projectile.SetActive(false);
        Debug.Log("PUM");
    }

    IEnumerator SpawnProjectiles()
    {
        while (true)
        {

            if (Projectiles.Count < 5)
            {
                Random.InitState((int)Time.time);
                GameObject p = Instantiate(projectile, this.transform);
                int pos_y = Random.Range(0, Screen.height);
                p.transform.position = new Vector3(Screen.width + 100, pos_y, 0);
                Projectiles.Add(p);
            }            
            yield return new WaitForSecondsRealtime(TimeBetweenPSpawn);
        }        
    }

    IEnumerator SpawnExplosiveProjectiles()
    {
        while (true)
        {
            if (ExplosiveProjectiles.Count < 2)
            {
                Random.InitState((int)Time.time);
                GameObject p = Instantiate(explosiveProjectile, this.transform);
                int pos_y = Random.Range(0, Screen.height);
                //p.transform.position = new Vector3(Random.Range(Screen.width*-1, Screen.width), Random.Range(Screen.height*-1, Screen.height));
                p.transform.position = new Vector3(Screen.width, Random.Range(0, Screen.height));
                ExplosiveProjectiles.Add(p);
            }
            yield return new WaitForSecondsRealtime(TimeBetweenPSpawn);
        }
    }

    public void SpawnMiniProjectiles(Transform OrPos)
    {
        if (MiniProjectiles.Count<24)
        {
            for (int i = 0; i < 4; i++)
            {
                GameObject p = Instantiate(miniProjectile, this.transform);
                p.transform.position = OrPos.position;
                switch (i)
                {
                    case 0:
                        TrackingMiniPos.Add(new Vector3(p.transform.position.x - Screen.height, p.transform.position.y - Screen.height, 0));
                        break;
                    case 1:
                        TrackingMiniPos.Add(new Vector3(p.transform.position.x + Screen.height, p.transform.position.y + Screen.height, 0));
                        p.transform.Rotate(0.0f, 0.0f, -180f, Space.Self);
                        break;
                    case 2:
                        TrackingMiniPos.Add(new Vector3(p.transform.position.x + Screen.height, p.transform.position.y - Screen.height, 0));
                        p.transform.Rotate(0.0f, 0.0f, -270f, Space.Self);
                        break;
                    case 3:
                        TrackingMiniPos.Add(new Vector3(p.transform.position.x - Screen.height, p.transform.position.y + Screen.height, 0));
                        p.transform.Rotate(0.0f, 0.0f, -90f, Space.Self);
                        break;
                    default:
                        break;
                }
                MiniProjectiles.Add(p);
            }
        }
    }

    IEnumerator SpawnTrackProjectiles()
    {
        while (true)
        {
            if (TrackProjectiles.Count < 2)
            {
                Random.InitState((int)Time.time);                
                GameObject p = Instantiate(trackProjectile, this.transform);               
                p.transform.position = new Vector3(Random.Range(0, Screen.width), Random.Range(0, Screen.height));
                if (p.transform.position.x > player.transform.position.x && p.transform.position.y > player.transform.position.y)
                {
                    p.gameObject.transform.Rotate(0.0f, 0.0f, 90f, Space.Self);
                    TrackingPos.Add(new Vector3(player.transform.position.x - Screen.width, p.transform.position.y - Screen.width, 0));
                    //Debug.Log("Player pos" + TrackingPos[TrackingPos.Count-1]);
                }
                else if(p.transform.position.x < player.transform.position.x && p.transform.position.y < player.transform.position.y)
                {
                    p.gameObject.transform.Rotate(0.0f, 0.0f, -90f, Space.Self);
                    TrackingPos.Add(new Vector3(player.transform.position.x + Screen.width, p.transform.position.y + Screen.width, 0));
                    //Debug.Log("Player pos" + TrackingPos[TrackingPos.Count-1]);
                }
                else if (p.transform.position.x < player.transform.position.x && p.transform.position.y > player.transform.position.y)
                {
                    p.gameObject.transform.Rotate(0.0f, 0.0f, -180f, Space.Self);
                    TrackingPos.Add(new Vector3(player.transform.position.x + Screen.width, p.transform.position.y - Screen.width, 0));
                    //Debug.Log("Player pos" + TrackingPos[TrackingPos.Count-1]);
                }
                else
                {
                    //p.gameObject.transform.Rotate(0.0f, 0.0f, -360f, Space.Self);
                    TrackingPos.Add(new Vector3(player.transform.position.x - Screen.width, p.transform.position.y + Screen.width, 0));
                    //Debug.Log("Player pos" + TrackingPos[TrackingPos.Count-1]);
                }
                TrackProjectiles.Add(p);
            }
            yield return new WaitForSecondsRealtime(TimeBetweenPSpawn);
        }
    }

    IEnumerator SpawnObstacles()
    {
        while (true)
        {
            if (Obstacles.Count < 5)
            {
                Random.InitState((int)Time.time);
                GameObject o = Instantiate(obstacle, this.transform);
                int pos_y = Random.Range(0, Screen.height);
                o.transform.position = new Vector3(Screen.width + 100, pos_y, 0);
                Obstacles.Add(o);
            }
            yield return new WaitForSecondsRealtime(Random.Range(0,TimeBetweenOSpawn));
        }
    }

    IEnumerator MoveProjectile()
    {
        while (true)
        {
            int x = Projectiles.Count;
            for (int i = 0; i < x; i++)
            {
                if (Projectiles[i].transform.position.x > 0)
                {
                    Projectiles[i].transform.Translate(-10, 0, 0);
                    yield return null;
                }
                else
                {
                    Destroy(Projectiles[i]);
                    Projectiles.Remove(Projectiles[i]);                    
                    break;                    
                }
            }
            yield return null;
        }        
    }

    IEnumerator MoveExplosiveProjectiles()
    {
        while (true)
        {
            int x = ExplosiveProjectiles.Count;
            for (int i = 0; i < x; i++)
            {
                if (ExplosiveProjectiles[i].activeSelf)
                {
                    if (ExplosiveProjectiles[i].transform.position.x > 0)
                    {
                        ExplosiveProjectiles[i].transform.Translate(-10, 0, 0);
                        yield return null;
                    }
                    else
                    {
                        ExplodeProjectile(ExplosiveProjectiles[i]);
                        Destroy(ExplosiveProjectiles[i]);
                        ExplosiveProjectiles.Remove(ExplosiveProjectiles[i]);
                        break;
                    }
                }
                else
                {
                    Destroy(ExplosiveProjectiles[i]);
                    ExplosiveProjectiles.Remove(ExplosiveProjectiles[i]);
                    break;
                }
            }
            yield return null;
        }
    }

    IEnumerator MoveTrackProjectile()
    {
        while (true)
        {
            int x = TrackProjectiles.Count;
            for (int i = 0; i < x; i++)
            {
                //Debug.Log("Moviendose hacia" + Vector3.MoveTowards(TrackProjectiles[i].transform.position, TrackingPos[i], 10));
                TrackProjectiles[i].transform.position = Vector3.MoveTowards(TrackProjectiles[i].transform.position, TrackingPos[i], 10);                
                if ((int)TrackProjectiles[i].transform.position.x == (int)TrackingPos[i].x)
                {
                    Destroy(TrackProjectiles[i]);
                    TrackingPos.Remove(TrackingPos[i]);
                    TrackProjectiles.Remove(TrackProjectiles[i]);
                    break;
                }
            }
            yield return null;
        }
    }
    
    IEnumerator MoveMiniProjectile()
    {
        while (true)
        {
            int x = MiniProjectiles.Count;
            for (int i = 0; i < x; i++)
            {
                //Debug.Log("Moviendose hacia" + Vector3.MoveTowards(TrackProjectiles[i].transform.position, TrackingPos[i], 10));
                MiniProjectiles[i].transform.position = Vector3.MoveTowards(MiniProjectiles[i].transform.position, TrackingMiniPos[i], 10);
                if ((int)MiniProjectiles[i].transform.position.x == (int)TrackingMiniPos[i].x)
                {
                    Destroy(MiniProjectiles[i]);
                    TrackingMiniPos.Remove(TrackingMiniPos[i]);
                    MiniProjectiles.Remove(MiniProjectiles[i]);
                    break;
                }
            }
            yield return null;
        }
    }

    IEnumerator MoveObstacles()
    {
        while (true)
        {
            int x = Obstacles.Count;
            for (int i = 0; i < x; i++)
            {
                if (Obstacles[i].transform.position.x > 0)
                {
                    Obstacles[i].transform.Translate(-10, 0, 0);
                    yield return new WaitForSecondsRealtime(.1f);
                }
                else
                {
                    Destroy(Obstacles[i]);
                    Obstacles.Remove(Obstacles[i]);
                    break;
                }
            }
            yield return null;
        }
    }

    IEnumerator PlayerMovement()
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
