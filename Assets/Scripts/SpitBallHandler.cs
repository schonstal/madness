using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpitBallHandler : MonoBehaviour {
  public GameObject prefab;

  Stack<GameObject> inactiveSpitBalls;

	void Start () {
    inactiveSpitBalls = new Stack<GameObject>();
	
	}
	
	void Update () {
	}

  public GameObject SpawnSpitBall() {
    GameObject spitBall;

    if (inactiveSpitBalls.Count > 0) {
      spitBall = inactiveSpitBalls.Pop();
    } else {
      spitBall = Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
    }  

    spitBall.SetActiveRecursively(true);
    return spitBall;	
  }

  public void PushSpitBall(GameObject spitBall) {
    spitBall.SetActiveRecursively(false);
    inactiveSpitBalls.Push(spitBall);
  }
}
