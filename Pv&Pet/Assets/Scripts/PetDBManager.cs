using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PetDBManager
{
    private static PetDBManager _instance;
    public static PetDBManager Instance
    {
        get { return _instance; }
    }

    public List<Pet> PetDB = new List<Pet>();

    public void Initialize()
    {
        _instance = this;
    }
    
    public void Set_PetDatabase()
    {
        for (int i = 0; PetDB.Count < 155; i++)
        {
            PetDB.Add(Resources.Load("Pets/Bull") as Pet);
            PetDB.Add(Resources.Load("Pets/Lunar Butterfly") as Pet);
            PetDB.Add(Resources.Load("Pets/Mantis") as Pet);
            PetDB.Add(Resources.Load("Pets/Roach") as Pet);
            PetDB.Add(Resources.Load("Pets/Scarab") as Pet);
            PetDB.Add(Resources.Load("Pets/Tick") as Pet);
            PetDB.Add(Resources.Load("Pets/Caterpillar") as Pet);
            PetDB.Add(Resources.Load("Pets/Giant Bug Centipede") as Pet);
            PetDB.Add(Resources.Load("Pets/Giant Bug Death Worm") as Pet);
            PetDB.Add(Resources.Load("Pets/Insects Dragon") as Pet);
            PetDB.Add(Resources.Load("Pets/Red Ant Knight") as Pet);
            PetDB.Add(Resources.Load("Pets/Waterstrider") as Pet);
            PetDB.Add(Resources.Load("Pets/Black Ant Archer") as Pet);
            PetDB.Add(Resources.Load("Pets/Black Ant Berserker") as Pet);
            PetDB.Add(Resources.Load("Pets/Black Ant Knight") as Pet);
            PetDB.Add(Resources.Load("Pets/Black Ant Mage") as Pet);
            PetDB.Add(Resources.Load("Pets/Black Ant Protector") as Pet);
            PetDB.Add(Resources.Load("Pets/Golem") as Pet);
            PetDB.Add(Resources.Load("Pets/Death Worm") as Pet);
            PetDB.Add(Resources.Load("Pets/Giant Bug Hercules") as Pet);
            PetDB.Add(Resources.Load("Pets/Hell Mantis") as Pet);
            PetDB.Add(Resources.Load("Pets/Swarm") as Pet);
            PetDB.Add(Resources.Load("Pets/Titan Tellia") as Pet);
            PetDB.Add(Resources.Load("Pets/Tridentpupa") as Pet);
            PetDB.Add(Resources.Load("Pets/Dryad Mini") as Pet);
            PetDB.Add(Resources.Load("Pets/Earth Dragon") as Pet);
            PetDB.Add(Resources.Load("Pets/Forest Spider") as Pet);
            PetDB.Add(Resources.Load("Pets/Imperial Widow") as Pet);
            PetDB.Add(Resources.Load("Pets/Six-Wing Fairy") as Pet);
            PetDB.Add(Resources.Load("Pets/Feral Kitsune") as Pet);
            PetDB.Add(Resources.Load("Pets/Rabbit Warriors Archer") as Pet);
            PetDB.Add(Resources.Load("Pets/Rabbit Warriors Bandit") as Pet);
            PetDB.Add(Resources.Load("Pets/Rabbit Warriors Knight") as Pet);
            PetDB.Add(Resources.Load("Pets/Seven Sins Greed") as Pet);
            PetDB.Add(Resources.Load("Pets/Wind Snake") as Pet);
            PetDB.Add(Resources.Load("Pets/Deer") as Pet);
            PetDB.Add(Resources.Load("Pets/Elf_Assasin") as Pet);
            PetDB.Add(Resources.Load("Pets/Elves Rapier") as Pet);
            PetDB.Add(Resources.Load("Pets/Elves Rogue Elf") as Pet);
            PetDB.Add(Resources.Load("Pets/Elves Spellcaster") as Pet);
            PetDB.Add(Resources.Load("Pets/Fairy Filia") as Pet);
            PetDB.Add(Resources.Load("Pets/Arcane Golem") as Pet);
            PetDB.Add(Resources.Load("Pets/Gemstone Fire") as Pet);
            PetDB.Add(Resources.Load("Pets/Gemstone Thunder") as Pet);
            PetDB.Add(Resources.Load("Pets/Gemstone Water") as Pet);
            PetDB.Add(Resources.Load("Pets/Gemstone Wind") as Pet);
            PetDB.Add(Resources.Load("Pets/Orb Fire") as Pet);
            PetDB.Add(Resources.Load("Pets/Orb Frost") as Pet);
            PetDB.Add(Resources.Load("Pets/Orb Thunder") as Pet);
            PetDB.Add(Resources.Load("Pets/Orb Wind") as Pet);
            PetDB.Add(Resources.Load("Pets/Elemental Earth Spirit Tellia") as Pet);
            PetDB.Add(Resources.Load("Pets/Elemental Goddess Airi") as Pet);
            PetDB.Add(Resources.Load("Pets/Elemental Goddess Flora") as Pet);
            PetDB.Add(Resources.Load("Pets/Elemental Goddess imp") as Pet);
            PetDB.Add(Resources.Load("Pets/Elemental Goddess Yukia") as Pet);
            PetDB.Add(Resources.Load("Pets/Elemental Ice Spirit Helida") as Pet);
            PetDB.Add(Resources.Load("Pets/Elemental Spirit Fire Blazia") as Pet);
            PetDB.Add(Resources.Load("Pets/Elemental Wind Spirit Tempestia") as Pet);
            PetDB.Add(Resources.Load("Pets/Dragon King Blue") as Pet);
            PetDB.Add(Resources.Load("Pets/Dragon King Brown") as Pet);
            PetDB.Add(Resources.Load("Pets/Dragon King Green") as Pet);
            PetDB.Add(Resources.Load("Pets/Dragon King Red") as Pet);
            PetDB.Add(Resources.Load("Pets/Egypt Archer") as Pet);
            PetDB.Add(Resources.Load("Pets/Egypt Axe") as Pet);
            PetDB.Add(Resources.Load("Pets/Egypt Chariot") as Pet);
            PetDB.Add(Resources.Load("Pets/Egypt Knight") as Pet);
            PetDB.Add(Resources.Load("Pets/Egypt Mage") as Pet);
            PetDB.Add(Resources.Load("Pets/Hieracosphinx") as Pet);
            PetDB.Add(Resources.Load("Pets/Cobra") as Pet);
            PetDB.Add(Resources.Load("Pets/Crocodile") as Pet);
            PetDB.Add(Resources.Load("Pets/Mummy") as Pet);
            PetDB.Add(Resources.Load("Pets/Sphinx") as Pet);
            PetDB.Add(Resources.Load("Pets/Pirate Bandit") as Pet);
            PetDB.Add(Resources.Load("Pets/Pirate Captain") as Pet);
            PetDB.Add(Resources.Load("Pets/Pirate Magic Scimitar") as Pet);
            PetDB.Add(Resources.Load("Pets/Pirate Monkey") as Pet);
            PetDB.Add(Resources.Load("Pets/Pirate Parrot") as Pet);
            PetDB.Add(Resources.Load("Pets/Pirate Skeleton") as Pet);
            PetDB.Add(Resources.Load("Pets/Turtle Golem") as Pet);
            PetDB.Add(Resources.Load("Pets/Mermaid") as Pet);
            PetDB.Add(Resources.Load("Pets/Mermaid Warrior Arliette") as Pet);
            PetDB.Add(Resources.Load("Pets/Mermaid Warrior Sasha") as Pet);
            PetDB.Add(Resources.Load("Pets/Mermaid Warrior Sion") as Pet);
            PetDB.Add(Resources.Load("Pets/Octopus") as Pet);
            PetDB.Add(Resources.Load("Pets/Piranos") as Pet);
            PetDB.Add(Resources.Load("Pets/Shark") as Pet);
            PetDB.Add(Resources.Load("Pets/Titan Aquos") as Pet);
            PetDB.Add(Resources.Load("Pets/Skeleton Archer") as Pet);
            PetDB.Add(Resources.Load("Pets/Skeleton Dragon") as Pet);
            PetDB.Add(Resources.Load("Pets/Skeleton Knight") as Pet);
            PetDB.Add(Resources.Load("Pets/Skeleton Knight Baron") as Pet);
            PetDB.Add(Resources.Load("Pets/Skeleton Mage") as Pet);
            PetDB.Add(Resources.Load("Pets/Skull Knight Xoer") as Pet);
            PetDB.Add(Resources.Load("Pets/Black Cat") as Pet);
            PetDB.Add(Resources.Load("Pets/Pumpkin") as Pet);
            PetDB.Add(Resources.Load("Pets/Pumpkin Gentleman") as Pet);
            PetDB.Add(Resources.Load("Pets/Pumpkin mini") as Pet);
            PetDB.Add(Resources.Load("Pets/Stein Monster") as Pet);
            PetDB.Add(Resources.Load("Pets/Ultra Stein") as Pet);
            PetDB.Add(Resources.Load("Pets/Carnivorous Plant") as Pet);
            PetDB.Add(Resources.Load("Pets/Dryads Archer") as Pet);
            PetDB.Add(Resources.Load("Pets/Dryads Mage") as Pet);
            PetDB.Add(Resources.Load("Pets/Dryads Warrior") as Pet);
            PetDB.Add(Resources.Load("Pets/Hydra") as Pet);
            PetDB.Add(Resources.Load("Pets/Yggdrasil") as Pet);
            PetDB.Add(Resources.Load("Pets/Toxic Root") as Pet);
            PetDB.Add(Resources.Load("Pets/Undead Claw Knight") as Pet);
            PetDB.Add(Resources.Load("Pets/Undead Gigaraven") as Pet);
            PetDB.Add(Resources.Load("Pets/Undead Skull Tree") as Pet);
            PetDB.Add(Resources.Load("Pets/Undead Walker") as Pet);
            PetDB.Add(Resources.Load("Pets/Undead Warrior") as Pet);
            PetDB.Add(Resources.Load("Pets/Undead Wolf") as Pet);
            PetDB.Add(Resources.Load("Pets/Banshee") as Pet);
            PetDB.Add(Resources.Load("Pets/Dark Axe Warrior") as Pet);
            PetDB.Add(Resources.Load("Pets/Dark Healer") as Pet);
            PetDB.Add(Resources.Load("Pets/Darkness Dullahan") as Pet);
            PetDB.Add(Resources.Load("Pets/Darkness Reaper") as Pet);
            PetDB.Add(Resources.Load("Pets/Shadow Knight") as Pet);
            PetDB.Add(Resources.Load("Pets/Dark Monk") as Pet);
            PetDB.Add(Resources.Load("Pets/Dragon Emperor Zalaras") as Pet);
            PetDB.Add(Resources.Load("Pets/Ghost Knight") as Pet);
            PetDB.Add(Resources.Load("Pets/Great witch") as Pet);
            PetDB.Add(Resources.Load("Pets/Succubus") as Pet);
            PetDB.Add(Resources.Load("Pets/Vampire") as Pet);
            PetDB.Add(Resources.Load("Pets/Witch") as Pet);
            PetDB.Add(Resources.Load("Pets/Eldritch Eyes") as Pet);
            PetDB.Add(Resources.Load("Pets/Eldritch slime type A") as Pet);
            PetDB.Add(Resources.Load("Pets/Eldritch slime type B") as Pet);
            PetDB.Add(Resources.Load("Pets/Eldritch slime type C") as Pet);
            PetDB.Add(Resources.Load("Pets/Eldritch slime type D") as Pet);
            PetDB.Add(Resources.Load("Pets/Eldritch slime type F") as Pet);
            PetDB.Add(Resources.Load("Pets/Kobold Paladin") as Pet);
            PetDB.Add(Resources.Load("Pets/Kobolds Dagger Kobold") as Pet);
            PetDB.Add(Resources.Load("Pets/Kobolt Rogue") as Pet);
            PetDB.Add(Resources.Load("Pets/kobolt ultra knight") as Pet);
            PetDB.Add(Resources.Load("Pets/Mage Kobold") as Pet);
            PetDB.Add(Resources.Load("Pets/Spear Kobold") as Pet);
            PetDB.Add(Resources.Load("Pets/Knight Axe Elite") as Pet);
            PetDB.Add(Resources.Load("Pets/Knight Blunderbuss Elite") as Pet);
            PetDB.Add(Resources.Load("Pets/Knight Spear Elite") as Pet);
            PetDB.Add(Resources.Load("Pets/Red Guard knuckles") as Pet);
            PetDB.Add(Resources.Load("Pets/Red Guard sniper") as Pet);
            PetDB.Add(Resources.Load("Pets/Red Guard warrior") as Pet);
            PetDB.Add(Resources.Load("Pets/Book Master") as Pet);
            PetDB.Add(Resources.Load("Pets/Innova") as Pet);
            PetDB.Add(Resources.Load("Pets/Novus") as Pet);
            PetDB.Add(Resources.Load("Pets/Red Guard Knight") as Pet);
            PetDB.Add(Resources.Load("Pets/Red guard Reaper") as Pet);
            PetDB.Add(Resources.Load("Pets/Abomination Hound") as Pet);
            PetDB.Add(Resources.Load("Pets/Abomination Tyrant") as Pet);
            PetDB.Add(Resources.Load("Pets/Abominations Scout") as Pet);
            PetDB.Add(Resources.Load("Pets/Cultist") as Pet);
            PetDB.Add(Resources.Load("Pets/God Yoggoth") as Pet);
            PetDB.Add(Resources.Load("Pets/King Yoggoth") as Pet);
            PetDB.Add(Resources.Load("Pets/Queen Yoggoth") as Pet);
            PetDB.Add(Resources.Load("Pets/Abomination Gazer") as Pet);
        }
    }
}
