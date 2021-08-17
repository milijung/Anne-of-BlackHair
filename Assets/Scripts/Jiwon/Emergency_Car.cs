using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Emergency_Car : MonoBehaviour
{
    Rigidbody2D rigid;
    public int car_speed;

    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //Move
        rigid.velocity = new Vector2(car_speed*(-1), rigid.velocity.y);

    }
}
