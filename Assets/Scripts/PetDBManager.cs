using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDBManager
{
    private static PetDBManager _instance;
    string resourcesPath;

    public static PetDBManager Instance
    {
        get { return _instance; }
    }

    public List<Pet> PetDB = new List<Pet>();

    public void Initialize()
    {
        _instance = this;
#if UNITY_ANDROID
        resourcesPath = Application.dataPath + "Resources/";
#endif
        resourcesPath = "";
    }

    public void Set_PetDatabase()
    {
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Bull") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Lunar Butterfly") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Mantis") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Roach") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Scarab") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Tick") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Caterpillar") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Giant Bug Centipede") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Giant Bug Death Worm") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Insects Dragon") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Red Ant Knight") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Waterstrider") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Black Ant Archer") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Black Ant Berserker") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Black Ant Knight") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Black Ant Mage") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Black Ant Protector") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Golem") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Death Worm") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Giant Bug Hercules") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Hell Mantis") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Swarm") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Titan Tellia") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Tridentpupa") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Dryad Mini") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Earth Dragon") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Forest Spider") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Imperial Widow") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Six-Wing Fairy") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Feral Kitsune") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Rabbit Warriors Archer") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Rabbit Warriors Bandit") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Rabbit Warriors Knight") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Seven Sins Greed") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Wind Snake") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Deer") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Elf Assasin") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Elves Rapier") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Elves Rogue Elf") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Elves Spellcaster") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Fairy Fili") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Arcane Golem") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Gemstone Fire") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Gemstone Thunder") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Gemstone Water") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Gemstone Wind") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Orb Fire") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Orb Frost") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Orb Thunder") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Orb Wind") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Elemental Earth Spirit Tellia") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Elemental Goddess Airi") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Elemental Goddess Flora") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Elemental Goddess imp") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Elemental Goddess Yukia") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Elemental Ice Spirit Helida") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Elemental Spirit Fire Blazia") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Elemental Wind Spirit Tempestia") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Dragon King Blue") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Dragon King Brown") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Dragon King Green") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Dragon King Red") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Egypt Archer") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Egypt Axe") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Egypt Chariot") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Egypt Knight") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Egypt Mage") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Hieracosphinx") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Cobra") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Crocodile") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Mummy") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Sphinx") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Pirate Bandit") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Pirate Captain") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Pirate Magic Scimitar") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Pirate Monkey") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Pirate Parrot") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Pirate Skeleton") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Turtle Golem") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Mermaid") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Mermaid Warrior Arliette") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Mermaid Warrior Sasha") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Mermaid Warrior Sion") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Octopus") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Piranos") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Shark") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Titan Aquos") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Skeleton Archer") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Skeleton Dragon") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Skeleton Knight") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Skeleton Knight Baron") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Skeleton Mage") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Skull Knight Xoer") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Black Cat") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Pumpkin") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Pumpkin Gentleman") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Pumpkin mini") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Stein Monster") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Ultra Stein") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Carnivorous Plant") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Dryads Archer") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Dryads Mage") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Dryads Warrior") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Hydra") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Yggdrasil") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Toxic Root") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Undead Claw Knight") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Undead Gigaraven") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Undead Skull Tree") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Undead Walker") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Undead Warrior") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Undead Wolf") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Banshee") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Dark Axe Warrior") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Dark Healer") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Darkness Dullahan") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Darkness Reaper") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Shadow Knight") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Dark Monk") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Dragon Emperor Zalaras") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Ghost Knight") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Great Witch") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Succubus") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Vampire") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Witch") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Eldritch Eyes") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Eldritch slime type A") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Eldritch slime type B") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Eldritch slime type C") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Eldritch slime type D") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Eldritch slime type F") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Kobold Paladin") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Kobold Dagger") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Kobold Rogue") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/kobold ultra knight") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Mage Kobold") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Spear Kobold") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Knight Axe Elite") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Knight Blunderbuss Elite") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Knight Spear Elite") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Red Guard knuckles") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Red Guard sniper") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Red Guard warrior") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Book Master") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Innova") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Novus") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Red Guard Knight") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Red guard Reaper") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Abomination Hound") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Abomination Tyrant") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Abominations Scout") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Cultist") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/God Yoggoth") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/King Yoggoth") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Queen Yoggoth") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Abomination Gazer") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Rock golem") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Toucan Panther") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Gunblin") as Pet);
            PetDB.Add(Resources.Load(resourcesPath + "Pets/Mimic Sword") as Pet);
    }

    public Pet InitializePet(string PetName,int hp ,int str, int agy, int spe, int arm)
    {
        Pet auxPet;
        auxPet = PetDB.Find(x => x.PetName == PetName);
        auxPet.HP = hp;
        auxPet.Strength = str;
        auxPet.Agility = agy;
        auxPet.Speed = spe;
        auxPet.Armor = arm;
        return auxPet;
    }
}
