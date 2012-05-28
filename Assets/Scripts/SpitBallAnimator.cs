using UnityEngine;
using System.Collections;

public class SpitBallAnimator : MonoBehaviour {
  AnimationManager animationManager;

	// Use this for initialization
	void Start () {
    animationManager = GetComponent<AnimationManager>() as AnimationManager;
    animationManager.AddAnimation("Flicker", new int[] {0,1}, 15f);
	}
	
	// Update is called once per frame
	void Update () {
    animationManager.Play("Flicker");
	}
}
