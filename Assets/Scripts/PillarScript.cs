using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PillarScript : MonoBehaviour
{
    #region Editor Fields
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private Vector2 speedRandomRange;

    [SerializeField]
    private float fallSpeed;
    [SerializeField]
    private float zFallPos;
    [SerializeField]
    private float chanceToFall;
    #endregion
    #region Private Fields
    private Rigidbody _rb;
    private PlayerController _player;
    private float fallDir;
    private bool goingToFall;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _player = GameObject.FindObjectOfType<PlayerController>().GetComponent<PlayerController>();
        goingToFall = Random.Range(0, 100) < chanceToFall;
        if (_player.transform.position.x > transform.position.x)
        {
            fallDir = -1;
        }
        else
        {
            fallDir = 1;
        }

        Vector3 newScale = transform.localScale;
        float factor = Random.Range(1,2);
        newScale.x *= factor;
        newScale.z *= factor;
        transform.localScale = newScale;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rb.velocity = (Vector3.back * Time.deltaTime * moveSpeed * 100);
        if(transform.position.z <= -1)
        {
            Destroy(gameObject);
        }

        if (_player.scoreReference > 1000 && transform.position.z < zFallPos && goingToFall)
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, 0, 70*fallDir), Time.deltaTime * (fallSpeed*(_player.globalSpeedFactor/2)));
        }
    }

    public void SetSpeed(float speed)
    {
        moveSpeed *= speed;
        moveSpeed *= Random.Range(speedRandomRange.x,speedRandomRange.y);
        if (GetComponent<Animator>())
        {
            GetComponent<Animator>().speed *= speed;
        }
    }
}
