using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Controll Script for Item

public class ItemController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Slot slot = new Slot();
        slot.init();
    }

    // Update is called once per frame
    void Update()
    {
        // if anne gets new item
        //      check the stack (item_slot)
        if (!slot.full())
        {
            item_type new_item = slot.new_random_item();
            int item_number = slot.item_stack.Count++;

            if (new_item == Slot.item_type.red_wig)
            {
                Debug.Log("red wig");
                // item slot UI 
                // add new_item to ( item_number slot ) 
            }
            else if (new_item == Slot.item_type.slate)
            {
                Debug.Log("slate");
                // item slot UI 
                // add new_item to ( item_number slot ) 
            }
            else if (new_item == Slot.item_type.hat)
            {
                Debug.Log("hat");
                // item slot UI 
                // add new_item to ( item_number slot ) 
            }
            else if (new_item == Slot.item_type.snail)
            {
                Debug.Log("snail");
                // item slot UI 
                // add new_item to ( item_number slot ) 
            }
            else if (new_item == Slot.item_type.eraser)
            {
                Debug.Log("eraser");
                // item slot UI 
                // add new_item to ( item_number slot ) 
            }
        }
        
        // if anne uses item
        //      check the stack is not empty
        //      if (!empty(item_slot))
        //          slot.use_item()

    }
}

public class Slot
{
    enum item_type
    {
        red_wig,
        slate,
        hat,
        snail,
        eraser
    }

    Stack<int> item_stack;

    public void init()
    {
        item_stack = new Stack<int>();
    }

    bool empty()
    {
        if (item_stack.Count == 0)
            return true;
        else return false;
    }

    bool full()
    {
        if (item_stack.Count == 2)
            return true;
        else return false;
    }

    item_type new_random_item()
    {
        // randomly initialize type
        item_type itemitem = new item_type();
        
        /*
        Random rand = new Random();
        int random = rand.Next(5); // 0~4
        */
        itemitem = randomEnum(Slot.item_type);

        item_stack.Push((int)itemitem);

        return itemitem;
    }

    void use_item()
    {
        int item_pop = item_stack.Pop();
    }
}
