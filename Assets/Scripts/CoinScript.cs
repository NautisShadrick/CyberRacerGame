using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinScript : MonoBehaviour
{

    #region Editor Fields
    [SerializeField]
    private GameObject coin;
    [SerializeField]
    private float chanceToSpawn;
    #endregion
    #region Private Fields
    GameObject newCoin;
    #endregion

    private void Start()
    {
        float roll = Random.Range(1,100);
        if(roll < chanceToSpawn)
        {
            newCoin = Instantiate(coin, transform.position, coin.transform.rotation);
        }
    }

    private void Update()
    {
        if (newCoin)
        {
            newCoin.transform.position = transform.position;
            if(newCoin.transform.position.z <= 1)
            {
                Destroy(newCoin);
            }
        }

        if (transform.position.z <= 0)
        {
            Destroy(gameObject);
        }
    }
}
