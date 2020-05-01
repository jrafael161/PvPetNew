﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesDBmanager
{
    private static AbilitiesDBmanager _instance;
    public static AbilitiesDBmanager Instance
    {
        get { return _instance; }
    }

    public List<Ability> AbilitiesDB = new List<Ability>();

    public void Initialize()
    {
        _instance = this;
    }

    public void Set_AbilitiesDatabase()
    {
        for (int i = 0; AbilitiesDB.Count < 5; i++)
        {
            AbilitiesDB.Add(Resources.Load("Abilities/Life Restoration") as Ability);
            AbilitiesDB.Add(Resources.Load("Abilities/Phoenix Rise") as Ability);
            AbilitiesDB.Add(Resources.Load("Abilities/Weapon Slash") as Ability);
            AbilitiesDB.Add(Resources.Load("Abilities/Life Orb") as Ability);
            AbilitiesDB.Add(Resources.Load("Abilities/Strength Orb") as Ability);
            AbilitiesDB.Add(Resources.Load("Abilities/Speed Orb") as Ability);
            AbilitiesDB.Add(Resources.Load("Abilities/Agility Orb") as Ability);
        }
    }
}