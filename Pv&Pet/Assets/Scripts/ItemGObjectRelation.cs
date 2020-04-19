using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemGObjectRelation
{
    public Item item;
    public  GameObject itemGObject;

    public ItemGObjectRelation(Item IT,GameObject ITGO)
    {
        item = IT;
        itemGObject = ITGO;
    }
}
