using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField]
    private string horizontalInput;
    private Quaternion newRot;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw(horizontalInput) < 0)
        {
            newRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0,0,-10),Time.deltaTime * 5);
        }
        else if(Input.GetAxisRaw(horizontalInput) > 0)
        {
            newRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 10), Time.deltaTime * 5);
        }
        else
        {
            newRot = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 0), Time.deltaTime * 5);

        }
        transform.rotation = newRot;
    }
}
