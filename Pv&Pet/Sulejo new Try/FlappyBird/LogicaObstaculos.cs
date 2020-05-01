using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogicaObstaculos : MonoBehaviour
{
	public float tiempoMax = 1; 
	private float tiempoInicial = 0;
	public GameObject obstaculo;
	public float altura;
    // Start is called before the first frame update
    void Start()
    {
        GameObject obstaculoNuevo = Instantiate(obstaculo);
		obstaculoNuevo.transform.position = transform.position+ new Vector3(0,0,0);
		Destroy(obstaculoNuevo,20);
    }

    // Update is called once per frame
    void Update()
    {
		if(LogicaScore.puntaje <=0 )
				MovePipe.speed=.2F;
		else if(LogicaScore.puntaje%2 == 0 & tiempoInicial>tiempoMax)
		{
				MovePipe.speed+=.2F;
		}
		
		if(tiempoInicial>tiempoMax)
		{
			GameObject obstaculoNuevo = Instantiate(obstaculo);
			obstaculoNuevo.transform.position = transform.position+ new Vector3(0,Random.Range(-altura,altura),0);
			Destroy(obstaculoNuevo,20);
			tiempoInicial=0;
		}
		else
		{
			tiempoInicial+=Time.deltaTime;
		}
		
		
    }
}
