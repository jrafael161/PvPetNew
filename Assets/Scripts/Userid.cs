using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Userid : MonoBehaviour
{
    public Text userid;
    // Start is called before the first frame update
    void Start()
    {
        userid.text = GameController.userid;
    }

    // Update is called once per frame
    void Update()
    {
        //userid.text = GameController.userid;
    }
}
