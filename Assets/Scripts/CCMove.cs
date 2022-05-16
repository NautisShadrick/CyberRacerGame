using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CCMove : MonoBehaviour
{
    private Transform player;

    // Start is called before the first frame update
    void Start()
    {
        if(!player)
        {
            try
            {
                player = GameObject.FindObjectOfType<PlayerController>().transform;
            }
            catch
            {
                return;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(player)
        {
            transform.position = player.position;
        }
    }
}
