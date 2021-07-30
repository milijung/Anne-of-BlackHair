using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemController : MonoBehaviour
{
    public Sprite[] ItemImage;
    public GameObject item0,item1;
    Slot item_slot;
    Item item;

    // Start is called before the first frame update
    void Start()
    {
        item_slot = new Slot();
        item_slot.slot_init();  
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void _get_new_item_on_the_road()
    {
        // Check the slot 
        if( item_slot.full() ) return;

        item = new Item();
        Item.item_type typetype = item.new_random_item(item_slot);

        if ( item_slot.item_have() == 1 )
        {
            // item0.Sprite
            item0.GetComponent<SpriteRenderer>().sprite = ItemImage[(int)typetype];

        }
        else if ( item_slot.item_have() == 2 )
        {
            // item1.Sprite
            item1.GetComponent<SpriteRenderer>().sprite = ItemImage[(int)typetype];
        }
                    
    }

    public void _use_item_in_the_slot()
    {
        // Check the slot
        //if( item_slot.empty() ) return;

        Item.item_type typetype = (Item.item_type) item_slot.stack.Pop();

        if ( item_slot.item_have() == 1 )
        {
            // item1.Sprite
            item1.GetComponent<SpriteRenderer>().sprite = null;

        }
        else if ( item_slot.item_have() == 0 )
        {
            // item0.Sprite
            item0.GetComponent<SpriteRenderer>().sprite = null;
        }

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