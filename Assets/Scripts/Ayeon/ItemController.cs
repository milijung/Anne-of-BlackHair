using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    Slot item_slot;
    Item item;

    // Start is called before the first frame update
    void Start()
    {
        item_slot = new Slot();
        item_slot.slot_init();
        //_get_new_item_on_the_road();
        
    }

    // Update is called once per frame
    void Update()
    {
        _get_new_item_on_the_road();
        _use_item_in_the_slot();
    }

    void _get_new_item_on_the_road()
    {
        // Check the slot 
        if (!item_slot.full())
        {
            item = new Item();
            Item.item_type typetype = item.new_random_item(item_slot);
            
            if (typetype == Item.item_type.red_wig)
            {
                Debug.Log("GET RED WIG");
            }
            else if (typetype == Item.item_type.slate)
            {
                Debug.Log("GET SLATE");
            }
            else if (typetype == Item.item_type.hat)
            {
                Debug.Log("GET HAT");
            }
            else if (typetype == Item.item_type.snail)
            {
                Debug.Log("GET SNAIL");
            }
            else if (typetype == Item.item_type.eraser)
            {
                Debug.Log("GET ERASER");
            }
        }
    }

    void _use_item_in_the_slot()
    {
        // Check the slot
        if (!item_slot.empty() && !item_slot.full())
        {
            Item.item_type typetype = (Item.item_type) item_slot.stack.Pop();
            if (typetype == Item.item_type.red_wig)
            {
                Debug.Log("USE RED WIG");
            }
            else if (typetype == Item.item_type.slate)
            {
                Debug.Log("USE SLATE");
            }
            else if (typetype == Item.item_type.hat)
            {
                Debug.Log("USE HAT");
            }
            else if (typetype == Item.item_type.snail)
            {
                Debug.Log("USE SNAIL");
            }
            else if (typetype == Item.item_type.eraser)
            {
                Debug.Log("USE ERASER");
            }
        }
    }
}

public class Item
{
    public enum item_type
    {
        red_wig,
        slate,
        hat,
        snail,
        eraser
    }

    public item_type new_random_item(Slot slot)
    {
        item_type new_item;
        new_item = (item_type)Random.Range(0,4);

        // store new item in slot
        slot.stack.Push((int)new_item);
    
        return new_item;
    }
}

public class Slot
{
    public Stack<int> stack;

    public void slot_init()
    {
        stack = new Stack<int>();
    }

    public int item_have()
    {
        return stack.Count;
    }

    public bool empty()
    {
        return (stack.Count == 0);
    }

    public bool full()
    {
        return (stack.Count == 2);
    }
}