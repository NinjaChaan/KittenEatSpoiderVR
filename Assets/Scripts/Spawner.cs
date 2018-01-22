using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {
    public GameObject spider;
    public List<GameObject> spiderList;
	// Use this for initialization
	void Start () {
        StartCoroutine(Spawn());
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator Spawn()
    {
        while (true)
        {
            if (spiderList.Count < 10)
            {
                yield return new WaitForSeconds(Random.Range(2, 5));
                Vector3 spawnPos = Random.insideUnitCircle * 5;
                spawnPos = new Vector3(spawnPos.x, 0.1f, spawnPos.z);
                GameObject spoider = Instantiate(spider, spawnPos, Quaternion.identity);
                spiderList.Add(spoider);
            }
            yield return null;
        }
    }
}
