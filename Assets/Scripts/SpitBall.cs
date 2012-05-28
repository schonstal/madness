using UnityEngine;
using System.Collections;

public class SpitBall : MonoBehaviour {
  public float timeToLive = 5.0f;

  SpitBallHandler ballHandler;
  float lifeTimer = 0f;

	void Start() {
    ballHandler = FindObjectOfType(typeof(SpitBallHandler)) as SpitBallHandler;
	}
	
	// Update is called once per frame
	void Update () {
    if(ballHandler == null)
      ballHandler = FindObjectOfType(typeof(SpitBallHandler)) as SpitBallHandler;

    lifeTimer += Time.deltaTime;
    if(lifeTimer >= timeToLive) {
      lifeTimer = 0;
      ballHandler.PushSpitBall(gameObject);
    }
	}

  void OnCollisionEnter(Collision collision) {
    ballHandler.PushSpitBall(gameObject);
  }
}
