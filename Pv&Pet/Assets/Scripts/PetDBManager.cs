﻿using System.Collections;
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

    public void Set_PetDatabase()
    {
        for (int i = 0; PetDB.Count < 155; i++)
        {
            PetDB.Add(Resources.Load("Pet/Bull") as Pet);
            PetDB.Add(Resources.Load("Pet/Lunar Butterfly") as Pet);
            PetDB.Add(Resources.Load("Pet/Mantis") as Pet);
            PetDB.Add(Resources.Load("Pet/Roach") as Pet);
            PetDB.Add(Resources.Load("Pet/Scarab") as Pet);
            PetDB.Add(Resources.Load("Pet/Tick") as Pet);
            PetDB.Add(Resources.Load("Pet/Caterpillar") as Pet);
            PetDB.Add(Resources.Load("Pet/Giant Bug Centipede") as Pet);
            PetDB.Add(Resources.Load("Pet/Giant Bug Death Worm") as Pet);
            PetDB.Add(Resources.Load("Pet/Insects Dragon") as Pet);
            PetDB.Add(Resources.Load("Pet/Red Ant Knight") as Pet);
            PetDB.Add(Resources.Load("Pet/Waterstrider") as Pet);
            PetDB.Add(Resources.Load("Pet/Black Ant Archer") as Pet);
            PetDB.Add(Resources.Load("Pet/Black Ant Berserker") as Pet);
            PetDB.Add(Resources.Load("Pet/Black Ant Knight") as Pet);
            PetDB.Add(Resources.Load("Pet/Black Ant Mage") as Pet);
            PetDB.Add(Resources.Load("Pet/Black Ant Protector") as Pet);
            PetDB.Add(Resources.Load("Pet/Golem") as Pet);
            PetDB.Add(Resources.Load("Pet/Death Worm") as Pet);
            PetDB.Add(Resources.Load("Pet/Giant Bug Hercules") as Pet);
            PetDB.Add(Resources.Load("Pet/Hell Mantis") as Pet);
            PetDB.Add(Resources.Load("Pet/Swarm") as Pet);
            PetDB.Add(Resources.Load("Pet/Titan Tellia") as Pet);
            PetDB.Add(Resources.Load("Pet/Tridentpupa") as Pet);
            PetDB.Add(Resources.Load("Pet/Dryad Mini") as Pet);
            PetDB.Add(Resources.Load("Pet/Earth Dragon") as Pet);
            PetDB.Add(Resources.Load("Pet/Forest Spider") as Pet);
            PetDB.Add(Resources.Load("Pet/Imperial Widow") as Pet);
            PetDB.Add(Resources.Load("Pet/Six-Wing Fairy") as Pet);
            PetDB.Add(Resources.Load("Pet/Feral Kitsune") as Pet);
            PetDB.Add(Resources.Load("Pet/Rabbit Warriors Archer") as Pet);
            PetDB.Add(Resources.Load("Pet/Rabbit Warriors Bandit") as Pet);
            PetDB.Add(Resources.Load("Pet/Rabbit Warriors Knight") as Pet);
            PetDB.Add(Resources.Load("Pet/Seven Sins Greed") as Pet);
            PetDB.Add(Resources.Load("Pet/Wind Snake") as Pet);
            PetDB.Add(Resources.Load("Pet/Deer") as Pet);
            PetDB.Add(Resources.Load("Pet/Elf_Assasin") as Pet);
            PetDB.Add(Resources.Load("Pet/Elves Rapier") as Pet);
            PetDB.Add(Resources.Load("Pet/Elves Rogue Elf") as Pet);
            PetDB.Add(Resources.Load("Pet/Elves Spellcaster") as Pet);
            PetDB.Add(Resources.Load("Pet/Fairy Filia") as Pet);
            PetDB.Add(Resources.Load("Pet/Arcane Golem") as Pet);
            PetDB.Add(Resources.Load("Pet/Gemstone Fire") as Pet);
            PetDB.Add(Resources.Load("Pet/Gemstone Thunder") as Pet);
            PetDB.Add(Resources.Load("Pet/Gemstone Water") as Pet);
            PetDB.Add(Resources.Load("Pet/Gemstone Wind") as Pet);
            PetDB.Add(Resources.Load("Pet/Orb Fire") as Pet);
            PetDB.Add(Resources.Load("Pet/Orb Frost") as Pet);
            PetDB.Add(Resources.Load("Pet/Orb Thunder") as Pet);
            PetDB.Add(Resources.Load("Pet/Orb Wind") as Pet);
            PetDB.Add(Resources.Load("Pet/Elemental Earth Spirit Tellia") as Pet);
            PetDB.Add(Resources.Load("Pet/Elemental Goddess Airi") as Pet);
            PetDB.Add(Resources.Load("Pet/Elemental Goddess Flora") as Pet);
            PetDB.Add(Resources.Load("Pet/Elemental Goddess imp") as Pet);
            PetDB.Add(Resources.Load("Pet/Elemental Goddess Yukia") as Pet);
            PetDB.Add(Resources.Load("Pet/Elemental Ice Spirit Helida") as Pet);
            PetDB.Add(Resources.Load("Pet/Elemental Spirit Fire Blazia") as Pet);
            PetDB.Add(Resources.Load("Pet/Elemental Wind Spirit Tempestia") as Pet);
            PetDB.Add(Resources.Load("Pet/Dragon King Blue") as Pet);
            PetDB.Add(Resources.Load("Pet/Dragon King Brown") as Pet);
            PetDB.Add(Resources.Load("Pet/Dragon King Green") as Pet);
            PetDB.Add(Resources.Load("Pet/Dragon King Red") as Pet);
            PetDB.Add(Resources.Load("Pet/Egypt Archer") as Pet);
            PetDB.Add(Resources.Load("Pet/Egypt Axe") as Pet);
            PetDB.Add(Resources.Load("Pet/Egypt Chariot") as Pet);
            PetDB.Add(Resources.Load("Pet/Egypt Knight") as Pet);
            PetDB.Add(Resources.Load("Pet/Egypt Mage") as Pet);
            PetDB.Add(Resources.Load("Pet/Hieracosphinx") as Pet);
            PetDB.Add(Resources.Load("Pet/Cobra") as Pet);
            PetDB.Add(Resources.Load("Pet/Crocodile") as Pet);
            PetDB.Add(Resources.Load("Pet/Mummy") as Pet);
            PetDB.Add(Resources.Load("Pet/Sphinx") as Pet);
            PetDB.Add(Resources.Load("Pet/Pirate Bandit") as Pet);
            PetDB.Add(Resources.Load("Pet/Pirate Captain") as Pet);
            PetDB.Add(Resources.Load("Pet/Pirate Magic Scimitar") as Pet);
            PetDB.Add(Resources.Load("Pet/Pirate Monkey") as Pet);
            PetDB.Add(Resources.Load("Pet/Pirate Parrot") as Pet);
            PetDB.Add(Resources.Load("Pet/Pirate Skeleton") as Pet);
            PetDB.Add(Resources.Load("Pet/Turtle Golem") as Pet);
            PetDB.Add(Resources.Load("Pet/Mermaid") as Pet);
            PetDB.Add(Resources.Load("Pet/Mermaid Warrior Arliette") as Pet);
            PetDB.Add(Resources.Load("Pet/Mermaid Warrior Sasha") as Pet);
            PetDB.Add(Resources.Load("Pet/Mermaid Warrior Sion") as Pet);
            PetDB.Add(Resources.Load("Pet/Octopus") as Pet);
            PetDB.Add(Resources.Load("Pet/Piranos") as Pet);
            PetDB.Add(Resources.Load("Pet/Shark") as Pet);
            PetDB.Add(Resources.Load("Pet/Titan Aquos") as Pet);
            PetDB.Add(Resources.Load("Pet/Skeleton Archer") as Pet);
            PetDB.Add(Resources.Load("Pet/Skeleton Dragon") as Pet);
            PetDB.Add(Resources.Load("Pet/Skeleton Knight") as Pet);
            PetDB.Add(Resources.Load("Pet/Skeleton Knight Baron") as Pet);
            PetDB.Add(Resources.Load("Pet/Skeleton Mage") as Pet);
            PetDB.Add(Resources.Load("Pet/Skull Knight Xoer") as Pet);
            PetDB.Add(Resources.Load("Pet/Black Cat") as Pet);
            PetDB.Add(Resources.Load("Pet/Pumpkin") as Pet);
            PetDB.Add(Resources.Load("Pet/Pumpkin Gentleman") as Pet);
            PetDB.Add(Resources.Load("Pet/Pumpkin mini") as Pet);
            PetDB.Add(Resources.Load("Pet/Stein Monster") as Pet);
            PetDB.Add(Resources.Load("Pet/Ultra Stein") as Pet);
            PetDB.Add(Resources.Load("Pet/Carnivorous Plant") as Pet);
            PetDB.Add(Resources.Load("Pet/Dryads Archer") as Pet);
            PetDB.Add(Resources.Load("Pet/Dryads Mage") as Pet);
            PetDB.Add(Resources.Load("Pet/Dryads Warrior") as Pet);
            PetDB.Add(Resources.Load("Pet/Hydra") as Pet);
            PetDB.Add(Resources.Load("Pet/Yggdrasil") as Pet);
            PetDB.Add(Resources.Load("Pet/Toxic Root") as Pet);
            PetDB.Add(Resources.Load("Pet/Undead Claw Knight") as Pet);
            PetDB.Add(Resources.Load("Pet/Undead Gigaraven") as Pet);
            PetDB.Add(Resources.Load("Pet/Undead Skull Tree") as Pet);
            PetDB.Add(Resources.Load("Pet/Undead Walker") as Pet);
            PetDB.Add(Resources.Load("Pet/Undead Warrior") as Pet);
            PetDB.Add(Resources.Load("Pet/Undead Wolf") as Pet);
            PetDB.Add(Resources.Load("Pet/Banshee") as Pet);
            PetDB.Add(Resources.Load("Pet/Dark Axe Warrior") as Pet);
            PetDB.Add(Resources.Load("Pet/Dark Healer") as Pet);
            PetDB.Add(Resources.Load("Pet/Darkness Dullahan") as Pet);
            PetDB.Add(Resources.Load("Pet/Darkness Reaper") as Pet);
            PetDB.Add(Resources.Load("Pet/Shadow Knight") as Pet);
            PetDB.Add(Resources.Load("Pet/Dark Monk") as Pet);
            PetDB.Add(Resources.Load("Pet/Dragon Emperor Zalaras") as Pet);
            PetDB.Add(Resources.Load("Pet/Ghost Knight") as Pet);
            PetDB.Add(Resources.Load("Pet/Great witch") as Pet);
            PetDB.Add(Resources.Load("Pet/Succubus") as Pet);
            PetDB.Add(Resources.Load("Pet/Vampire") as Pet);
            PetDB.Add(Resources.Load("Pet/Witch") as Pet);
            PetDB.Add(Resources.Load("Pet/Eldritch Eyes") as Pet);
            PetDB.Add(Resources.Load("Pet/Eldritch slime type A") as Pet);
            PetDB.Add(Resources.Load("Pet/Eldritch slime type B") as Pet);
            PetDB.Add(Resources.Load("Pet/Eldritch slime type C") as Pet);
            PetDB.Add(Resources.Load("Pet/Eldritch slime type D") as Pet);
            PetDB.Add(Resources.Load("Pet/Eldritch slime type F") as Pet);
            PetDB.Add(Resources.Load("Pet/Kobold Paladin") as Pet);
            PetDB.Add(Resources.Load("Pet/Kobolds Dagger Kobold") as Pet);
            PetDB.Add(Resources.Load("Pet/Kobolt Rogue") as Pet);
            PetDB.Add(Resources.Load("Pet/kobolt ultra knight") as Pet);
            PetDB.Add(Resources.Load("Pet/Mage Kobold") as Pet);
            PetDB.Add(Resources.Load("Pet/Spear Kobold") as Pet);
            PetDB.Add(Resources.Load("Pet/Knight Axe Elite") as Pet);
            PetDB.Add(Resources.Load("Pet/Knight Blunderbuss Elite") as Pet);
            PetDB.Add(Resources.Load("Pet/Knight Spear Elite") as Pet);
            PetDB.Add(Resources.Load("Pet/Red Guard knuckles") as Pet);
            PetDB.Add(Resources.Load("Pet/Red Guard sniper") as Pet);
            PetDB.Add(Resources.Load("Pet/Red Guard warrior") as Pet);
            PetDB.Add(Resources.Load("Pet/Book Master") as Pet);
            PetDB.Add(Resources.Load("Pet/Innova") as Pet);
            PetDB.Add(Resources.Load("Pet/Novus") as Pet);
            PetDB.Add(Resources.Load("Pet/Red Guard Knight") as Pet);
            PetDB.Add(Resources.Load("Pet/Red guard Reaper") as Pet);
            PetDB.Add(Resources.Load("Pet/Abomination Hound") as Pet);
            PetDB.Add(Resources.Load("Pet/Abomination Tyrant") as Pet);
            PetDB.Add(Resources.Load("Pet/Abominations Scout") as Pet);
            PetDB.Add(Resources.Load("Pet/Cultist") as Pet);
            PetDB.Add(Resources.Load("Pet/God Yoggoth") as Pet);
            PetDB.Add(Resources.Load("Pet/King Yoggoth") as Pet);
            PetDB.Add(Resources.Load("Pet/Queen Yoggoth") as Pet);
            PetDB.Add(Resources.Load("Pet/Abomination Gazer") as Pet);
        }
    }
}
