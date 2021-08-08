using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BerryController : MonoBehaviour
{
    public GameObject[] Berry_play;
    public static bool getBerryBox, getBerry, BumpOntheRoad = false;
    public List<Vector3> StartPosition;
    BerrySlot Berry_slot;
    // Start is called before the first frame update
    private void Start()
    {
        Berry_slot = new BerrySlot();
        Berry_slot.slot_init();
        
        for (int i = 0; i < Berry_play.Length; i++)
        {
            Berry_play[i].SetActive(false);
            Berry_slot.stack.Push(i);
            StartPosition.Add(Berry_play[i].transform.position);
        }
        
        StartCoroutine(BerryStart());
        StartCoroutine(BerryChange());
    }
    private void Update()
    {
        if (Berry_slot.empty())
        {
            GameManager.instance.GameOver();
        }
    }
    // Update is called once per frame
    public IEnumerator BerryStart()
    {
        while (true)
        {
            if (getBerryBox)
            {
                for (int i = 0; i < Berry_play.Length; i++)
                    Berry_play[i].SetActive(true);
                break;                
            }
            yield return null;
        }
        getBerryBox = false;
        StopCoroutine(BerryStart());
    }
    public IEnumerator BerryChange()
    {
        while (true)
        {
            if (getBerry)
            {
                if (Berry_slot.full()) yield return null;
                else
                {
                    Berry_play[Berry_slot.berry_have()].transform.position = StartPosition[Berry_slot.berry_have()];
                    Berry_play[Berry_slot.berry_have()].SetActive(true);
                    Berry_slot.stack.Push(Berry_slot.berry_have());
                }
                yield return new WaitForSeconds(0.1f);
                getBerry = false;
            }
            if (BumpOntheRoad)
            {
                if (Berry_slot.empty()) yield return null;
                else
                {
                    Berry_slot.stack.Pop();
                    while (true) 
                    {
                        if (Berry_play[Berry_slot.berry_have()].transform.position.y >= -8)
                        {
                            Berry_play[Berry_slot.berry_have()].transform.Translate(Vector2.down * 17 * Time.deltaTime);
                        }
                        else
                        {
                            Berry_play[Berry_slot.berry_have()].SetActive(false);
                            break;
                        }
                        yield return null;
                    }
                    

                }
                yield return new WaitForSeconds(0.1f);
                BumpOntheRoad = false;
            }
            yield return null;
        }
    }
}
public class BerrySlot
{
    public Stack<int> stack;
    public void slot_init()
    {
        stack = new Stack<int>();
    }

    public int berry_have()
    {
        return stack.Count;
    }

    public bool empty()
    {
        return (stack.Count == 0);
    }

    public bool full()
    {
        return (stack.Count == 3);
    }
}
