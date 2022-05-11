using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerScript : MonoBehaviour
{
    #region Editor Fields
    [SerializeField]
    private GameObject[] Pillars;
    [SerializeField]
    private Vector2 scaleRangeX;
    [SerializeField]
    private Vector2 scaleRangeZ;
    [SerializeField]
    private Vector2 spawnDelayRange;
    [SerializeField]
    private float spawnOdd;
    [SerializeField]
    private Transform[] spawners;

    [SerializeField]
    private float currentSpeedMult;
    #endregion

    #region Private Fields
    PlayerController _player;
    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _player = GetComponent<PlayerController>();

        StartCoroutine(SpawnPillarCoroutine(Random.Range(spawnDelayRange.x,spawnDelayRange.y)));
    }

    // Update is called once per frame
    void Update()
    {
        currentSpeedMult = _player.globalSpeedFactor;
    }

    private void SpawnPillar()
    {
        foreach (Transform spawner in spawners)
        {
            float newRoll = Random.Range(1f,100f);
            float shortRoll = Random.Range(1f,100f);
            int pillarDex;
            if(shortRoll < 20)
            {
                pillarDex = 1;
            }
            else
            {
                pillarDex = 0;
            }


            if (newRoll > spawnOdd)
            {
                GameObject newPillar = Instantiate(Pillars[pillarDex], spawner.position, Pillars[pillarDex].transform.rotation);

                Vector3 newScale = new Vector3(Random.Range(scaleRangeX.x,scaleRangeX.y),newPillar.transform.localScale.y,Random.Range(scaleRangeZ.x,scaleRangeZ.y));
                newPillar.transform.localScale = newScale;

                PillarScript ps = newPillar.GetComponent<PillarScript>();
                ps.SetSpeed(currentSpeedMult);

                //Destroy(newPillar, 10f);
            }
        }
    }

    public IEnumerator SpawnPillarCoroutine(float delay)
    {
        SpawnPillar();
        yield return new WaitForSeconds(delay);
        StartCoroutine(SpawnPillarCoroutine(Random.Range(spawnDelayRange.x, spawnDelayRange.y) / currentSpeedMult));
    }
}
