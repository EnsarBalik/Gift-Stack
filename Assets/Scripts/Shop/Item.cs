using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public enum ItemType
    {
        product1,
        product2,
        product3,
        product4,
        product5
    }

    public static int GetCost(ItemType itemType)
    {
        switch (itemType)
        {
            default:
            case ItemType.product1:
                return 0;
            case ItemType.product2:
                return 500;
            case ItemType.product3:
                return 150;
            case ItemType.product4:
                return 300;
            case ItemType.product5:
                return 600;
        }
    }
    
}