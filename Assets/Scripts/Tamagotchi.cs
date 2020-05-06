using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
public class Tamagotchi : MonoBehaviour
{
    public Slider Felicidad_bar;
    public Slider Hambre_bar;
    public Slider Limpieza_bar;


    // Start is called before the first frame update
    void Start()
    {
        int Felicidad = 1588464000;
        int Hambre = 1588464000;
        int Limpieza = 1588464000;
        Initialize_bars(Felicidad, Hambre, Limpieza);
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Initialize_bars(int Felicidad, int Hambre, int Limpieza)
    {
        int time = (int)new DateTimeOffset(DateTime.Now).ToUnixTimeSeconds();
        int since_fel = 100 - (((time - Felicidad) / 3600) * 5);
        int since_ham = 100 - (((time - Hambre) / 3600) * 5);
        int since_lim = 100 - (((time - Limpieza) / 3600) * 5);
        if (since_fel < 0)
            since_fel = 0;
        if (since_ham < 0)
            since_ham = 0;
        if (since_lim < 0)
            since_lim = 0;
        Felicidad_bar.value = since_fel;
        Hambre_bar.value = since_ham;
        Limpieza_bar.value = since_lim;
    }

    public void Dar_felicidad()
    {
        Felicidad_bar.value = 100;
    }
    public void Dar_comida()
    {
        Hambre_bar.value = 100;
    }
    public void Dar_limpieza()
    {
        Limpieza_bar.value = 100;
    }
}
