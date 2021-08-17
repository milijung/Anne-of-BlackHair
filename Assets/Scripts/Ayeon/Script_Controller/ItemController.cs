using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    public GameManager gameManager;
    public Booster_Controller booster_Controller;
    public SideMob_Controller Mob_motion;
    public SomoonGauge somoon;
    public Sprite[] ItemImage;
    public GameObject item0, item1, item2, useItemIMG;
    RectTransform ItemSave;
    public Text itemName;
    public float upSpeed = 1;
    // item0, item1 => slot_item
    // item2 => twinkle

    Slot item_slot;
    Item item;
    bool isBasket;

    private void UpSpeed()
    {
        if (!isBasket)
        {
            isBasket = true;
            upSpeed = 2f;
            gameManager.gameSpeed *= upSpeed;
            somoon.somoonContinue = false;
            booster_Controller.Collider_UnEnable();
            Invoke("ReturnSpeed", 3f);
        }
    }

    private void ReturnSpeed()
    {
        isBasket = false;
        gameManager.gameSpeed /= upSpeed;
        upSpeed = 1;
        somoon.somoonContinue = true;
    }

    // Start is called before the first frame update
    void Start()
    {
        item_slot = new Slot();
        item_slot.slot_init();
        useItemIMG.SetActive(false);
        isBasket = false;
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
        if (item0.GetComponent<Image>().sprite == null)
        {
            item0.SetActive(false);
        }
        if (item0.GetComponent<Image>().sprite != null)
        {
            item0.SetActive(true);
        }
        if (item1.GetComponent<Image>().sprite == null)
        {
            item1.SetActive(false);
        }
        if (item1.GetComponent<Image>().sprite != null)
        {
            item1.SetActive(true);
        }
    }

    public void _get_new_item_on_the_road()
    {
        // Check the slot 
        if (item_slot.full()) return;

        item = new Item();
        Item.item_type typetype = item.new_random_item(item_slot);

        if (item_slot.item_have() == 1)
        {
            // item0.Sprite
            item0.GetComponent<Image>().sprite = ItemImage[(int)typetype];

        }
        else if (item_slot.item_have() == 2)
        {
            // item1.Sprite
            item1.GetComponent<Image>().sprite = ItemImage[(int)typetype];
        }

    }

    public void _use_item_in_the_slot()
    {
        if (!useItemIMG.activeSelf)
        {
            Item.item_type typetype = (Item.item_type)item_slot.stack.Pop();
            if (item_slot.item_have() == 1)
            {
                // item1.Sprite
                item1.SetActive(false);
                item1.GetComponent<Image>().sprite = null;
            }
            else if (item_slot.item_have() == 0)
            {
                // item0.Sprite
                item0.SetActive(false);
                item0.GetComponent<Image>().sprite = null;
            }
            useItemIMG.GetComponent<Image>().sprite = ItemImage[(int)typetype];
            StartCoroutine(showItem());


            // ITEM USE FUNCTION 
            if (typetype == Item.item_type.red_wig)
            {

                itemName.text = "빨간가발 사용";
            }
            else if (typetype == Item.item_type.slate)
            {
                itemName.text = "석판 사용";
            }
            else if (typetype == Item.item_type.hat)
            {
                itemName.text = "모자 사용";
            }
            else if (typetype == Item.item_type.boost)
            {
                itemName.text = "바구니 사용";
                UpSpeed();
            }
            else if (typetype == Item.item_type.eraser)
            {
                itemName.text = "마녀의 열매 발동";
                somoon.LowerSomoon();
                Mob_motion.Set();
            }
        }
        else
            return;
    }
    IEnumerator showItem()
    {
        useItemIMG.SetActive(true);
        itemName.gameObject.SetActive(true);
        ItemSave = useItemIMG.GetComponent<RectTransform>();

        useItemIMG.SetActive(true);
        for (int i = 100; i <= 200; i += 20)
        {
            ItemSave.sizeDelta = new Vector2(i, i);
            yield return new WaitForSeconds(0.05f);
        }

        yield return new WaitForSeconds(1f);
        itemName.gameObject.SetActive(false);
        useItemIMG.SetActive(false);
        
        StopCoroutine(showItem());
    }
}

public class Item
{
    public enum item_type
    {
        red_wig,
        slate,
        hat,
        boost,
        eraser
    }

    public item_type new_random_item(Slot slot)
    {
        item_type new_item;
        new_item = (item_type)Random.Range(0, 5);

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