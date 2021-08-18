using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    GameObject _player;
    GameObject _twinkle;

    public GameManager gameManager;
    public Booster_Controller booster_Controller;
    public SideMob_Controller Mob_motion;
    public SomoonGauge somoon;
    public Sprite[] ItemImage;

    public GameObject GameObject_item0, GameObject_item1, useItemIMG;
    RectTransform ItemSave;
    public Text itemName;
    public float upSpeed = 1;
    // item0, item1 => slot_item


    Slot item_slot;
    Item item;
    bool isBasket;

    // Start is called before the first frame update
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

    void Start()
    {
        _player = GameObject.Find("Player");
        _twinkle = GameObject.Find("Twinkle");

        item_slot = new Slot();
        item_slot.slot_init();
        useItemIMG.SetActive(false);
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
        if (GameObject_item0.GetComponent<Image>().sprite == null)
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
            // slot_full()
            item1_type = item_slot.stack.Pop();
            item_slot.stack.Clear();
            // before : stack ( item0 - item1 )
            // after : stack ( item1 - new_item )

            item_slot.stack.Push(item1_type);
            item_slot.stack.Push(new_item_type);

            // item0 <- item1
            GameObject_item0.GetComponent<Image>().sprite = ItemImage[item1_type];

            // item1 <- new item
            GameObject_item1.GetComponent<Image>().sprite = ItemImage[new_item_type];
            
            return;
        }

        item_slot.stack.Push(new_item_type);

        if (item_slot.item_have() == 1)
            // item0.Sprite
            GameObject_item0.GetComponent<Image>().sprite = ItemImage[new_item_type];

        else if (item_slot.item_have() == 2)
            // item1.Sprite
            GameObject_item1.GetComponent<Image>().sprite = ItemImage[new_item_type];

    }


    public void _use_item_in_the_slot()
    {
        if (!useItemIMG.activeSelf)
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
            useItemIMG.GetComponent<Image>().sprite = ItemImage[(int)typetype];
            StartCoroutine(showItem());


            // ITEM USE FUNCTION 
            if (typetype == Item.item_type.wig)
            {
                itemName.text = "wig";
                _player.GetComponent<Animator>().SetBool("W",true);
                // twinkle on
                _twinkle.GetComponent<Animator>().SetBool("T",true);
            }
            else if (typetype == Item.item_type.hat)
            {
                itemName.text = "hat";
                 _player.GetComponent<Animator>().SetBool("H",true);
                // twinkle on
                _twinkle.GetComponent<Animator>().SetBool("T",true);
            }
            else if (typetype == Item.item_type.death_berry)
            {
                itemName.text = "death_berry";
                _player.GetComponent<Animator>().SetBool("G",true);
            }
            else if (typetype == Item.item_type.basket)
            {
                itemName.text = "basket";
                _player.GetComponent<Animator>().SetBool("B",true);
                // twinkle on
                _twinkle.GetComponent<Animator>().SetBool("T",true);
                UpSpeed();
            }
            else if (typetype == Item.item_type.eraser)
            {
                itemName.text = "eraser";
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
        wig,
        hat,
        death_berry,
        basket,
        eraser
    }

    public item_type new_random_item()
    {
        item_type new_item;
        new_item = (item_type)Random.Range(0, 5);

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