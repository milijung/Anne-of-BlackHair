using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{

    public Sprite[] ItemImage;
    public GameObject GameObject_item0, GameObject_item1, GameObject_twinkle;
    // GameObject_item0, GameObject_item1 => slot_item
    // GameObject_twinkle => twinkle

    Slot item_slot;
    Item item;

    // Start is called before the first frame update
    void Start()
    {
        item_slot = new Slot();
        item_slot.slot_init();
    }

    private void Update()
    {
        if (SwipeManager.doubleTap)
        {
            if (item_slot.item_have() != 0)
            {
                _use_item_in_the_slot();
            }
            return;
        }
        if(GameObject_item0.GetComponent<Image>().sprite == null)
        {
            GameObject_item0.SetActive(false);
        }
        if (GameObject_item0.GetComponent<Image>().sprite != null)
        {
            GameObject_item0.SetActive(true);
        }
        if (GameObject_item1.GetComponent<Image>().sprite == null)
        {
            GameObject_item1.SetActive(false);
        }
        if (GameObject_item1.GetComponent<Image>().sprite != null)
        {
            GameObject_item1.SetActive(true);
        }
    }

    public void _get_new_item_on_the_road()
    {
        item = new Item();

        int new_item_type;
        new_item_type = (int)item.new_random_item();
        
        int item1_type;

        // Update the slot item
        if (item_slot.full())
        {
            // 만약 아이템 슬롯이 꽉찬 상태 slot_full()
            item1_type = item_slot.stack.Pop();
            item_slot.stack.Clear();
            // before : stack ( item0 - item1 )
            // after : stack ( item1 - new_item )

            item_slot.stack.Push(item1_type);
            item_slot.stack.Push(new_item_type);

            // item0 <- item1
            GameObject_item0.GetComponent<Image>().sprite = ItemImage[item1_type];

            // item1 <- new item 으로 바꾸기
            GameObject_item1.GetComponent<Image>().sprite = ItemImage[new_item_type];
            
            return;
        }

        item_slot.stack.Push(new_item_type);

        if (item_slot.item_have() == 1)
        {
            // item0.Sprite
            GameObject_item0.GetComponent<Image>().sprite = ItemImage[new_item_type];

        }

        else if (item_slot.item_have() == 2)
        {
            // item1.Sprite
            GameObject_item1.GetComponent<Image>().sprite = ItemImage[new_item_type];
        }
    }

    public void _use_item_in_the_slot()
    {
        Item.item_type typetype = (Item.item_type)item_slot.stack.Pop();

        if (item_slot.item_have() == 1)
        {
            // item1.Sprite
            GameObject_item1.SetActive(false);
            GameObject_item1.GetComponent<Image>().sprite = null;

        }
        else if (item_slot.item_have() == 0)
        {
            // item0.Sprite
            GameObject_item0.SetActive(false);
            GameObject_item0.GetComponent<Image>().sprite = null;
        }

        // ITEM USE FUNCTION 
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

    public item_type new_random_item()
    {
        item_type new_item;
        new_item = (item_type)Random.Range(0, 4);

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