using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
  public GameObject prefab;
  public int numEnemies = 100;
  Stack<GameObject> inactiveEnemies;

	// Use this for initialization
	void Start () {
    inactiveEnemies = new Stack<GameObject>();

    GameObject enemy;
    for(int i = 0; i < numEnemies; i++) {
      if (inactiveEnemies.Count > 0) {
        enemy = inactiveEnemies.Pop();
      } else {
        enemy = Instantiate(prefab, new Vector3(Random.Range(-50,50), -38.3f, Random.Range(-50,50)), transform.rotation) as GameObject;
      }  
    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
