using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilitiesDBmanager
{
    private static AbilitiesDBmanager _instance;
    string resourcesPath;

    public static AbilitiesDBmanager Instance
    {
        get { return _instance; }
    }

    public List<Ability> AbilitiesDB = new List<Ability>();

    public void Initialize()
    {
        _instance = this;
#if UNITY_ANDROID
        resourcesPath = Application.dataPath + "Resources/";
#endif
        resourcesPath = "";
    }

    public void Set_AbilitiesDatabase()
    {
            AbilitiesDB.Add(Resources.Load(resourcesPath + "Abilities/Life Restoration") as Ability);
            AbilitiesDB.Add(Resources.Load(resourcesPath + "Abilities/Phoenix Rise") as Ability);
            AbilitiesDB.Add(Resources.Load(resourcesPath + "Abilities/Weapon Slash") as Ability);
            AbilitiesDB.Add(Resources.Load(resourcesPath + "Abilities/Life Orb") as Ability);
            AbilitiesDB.Add(Resources.Load(resourcesPath + "Abilities/Strength Orb") as Ability);
            AbilitiesDB.Add(Resources.Load(resourcesPath + "Abilities/Speed Orb") as Ability);
            AbilitiesDB.Add(Resources.Load(resourcesPath + "Abilities/Agility Orb") as Ability);
    }
}
