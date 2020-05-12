using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BasicMovement : MonoBehaviour
{
    public float speed = 5.0f;
    public int hits = 0;
    public int points = 0;

    private Rigidbody2D rb;
    private Vector2 screenBounds;
    // Start is called before the first frame update
    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(-speed, 0);
        screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }
    void Update()
    {
        if (transform.position.x < -screenBounds.x)
        {
            Text Lives = GameObject.Find("Canvas/bg_score/Lbl_v").GetComponent<Text>();
            int Livesint = int.Parse(Lives.text)-1;
            Lives.text = Livesint.ToString();
            Destroy(this.gameObject);
        }
    }

    public void HitSlime()
    {
        hits = hits - 1;
        if(hits == 0)
        {
            Text Score = GameObject.Find("Canvas/bg_score/Lbl_s").GetComponent<Text>();
            Text Nivel = GameObject.Find("Canvas/bg_score/Lbl_n").GetComponent<Text>();
            int Scoreint = int.Parse(Score.text) + points;
            int Nivelint = (Scoreint / 10 )+1;
            Score.text = Scoreint.ToString();
            Nivel.text = Nivelint.ToString();
            Destroy(this.gameObject);
        }
    }

}
