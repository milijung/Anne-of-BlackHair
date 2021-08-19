using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Booster_Controller : MonoBehaviour
{
    public BoxCollider[] obstacles;
    // Start is called before the first frame update
    public void Collider_UnEnable()
    {
        for(int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].enabled = false;
        }
        Invoke("Collider_Enable", 4f);
    }

    private void Collider_Enable()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            obstacles[i].enabled = true;
        }
    }
}
