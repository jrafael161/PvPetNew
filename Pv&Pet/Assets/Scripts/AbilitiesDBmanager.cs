using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesDBmanager
{
    public List<Ability> AbilitiesDB = new List<Ability>();

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
