using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
  public GameObject prefab;
  public int numEnemies = 1000;
  Stack<GameObject> inactiveEnemies;

	// Use this for initialization
	void Start () {
    inactiveEnemies = new Stack<GameObject>();

    GameObject enemy;
    for(int i = 0; i < numEnemies; i++) {
      if (inactiveEnemies.Count > 0) {
        enemy = inactiveEnemies.Pop();
      } else {
        enemy = Instantiate(prefab, new Vector3(Random.Range(-20,20), -38.3f, Random.Range(-20,20)), transform.rotation) as GameObject;
      }  
    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
