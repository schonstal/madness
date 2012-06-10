using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour {
  public GameObject prefab;
  public int numEnemies = 100;
  Stack<GameObject> inactiveEnemies;

	void Start() {
    inactiveEnemies = new Stack<GameObject>();

    GameObject enemy;
    enemy = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
	}
	

  void OnDrawGizmos() {
    Gizmos.DrawIcon(transform.position + new Vector3(0f,2.5f,0f), "EditorIcons/Enemy.png", false);
  }
}
