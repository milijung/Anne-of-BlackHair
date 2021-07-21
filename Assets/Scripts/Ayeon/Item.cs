using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
public class Item : MonoBehaviour
{
    /*
    slot item_slot;
    item item_in_slot;

    // Start is called before the first frame update
    void Start()
    {
        item_slot = new slot();
        item_slot.slot_init();

        item_in_slot = new item();

        Debug.Log(item_in_slot.new_random_item(item_slot));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void anne_get_item()
    {
        if(item_slot.item_have() == 0)
        {
            Debug.Log(item_in_slot.new_random_item(item_slot));
        }
        else if(item_slot.item_have() == 1)
        {
            Debug.Log(item_in_slot.new_random_item(item_slot));
        }
        
    }
}

public class slot
{
    Stack<int> slot_stack;

    public void slot_init()
    {
        slot_stack = new Stack<int>();
    }

    public int item_have()
    {
        return slot_stack.Count;
    }

}

public class item
{
    public enum item_type
    {
        red_wig,
        slate,
        hat,
        snail,
        eraser
    }

    public item_type new_random_item(slot SKIT)
    {
        item_type new_item;
        new_item = (item_type)Random.Range(0,4);

        return new_item;
    }

    */
//}
