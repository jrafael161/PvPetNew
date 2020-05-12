using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    static AgilityGame ag;

    private void Start()
    {
        ag = GameObject.FindObjectOfType<AgilityGame>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Player")
        {
            Debug.Log("El jugador choco con algo");
        }
        if (collision.collider.tag == "ExplosiveProjectiles")
        {
            Debug.Log("PUM");
            ag.ExplodeProjectile(collision.collider.gameObject);
            //Mandar a llamar la funcion que desperdigue el meteorito
        }
    }
}
