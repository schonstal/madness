using UnityEngine;
using System.Collections;

public class SpitterBrain : MonoBehaviour {
  AnimationManager animationManager;

	void Start () {
    animationManager = GetComponent<AnimationManager>() as AnimationManager;
    animationManager.AddAnimation("Fly", new int[] {0,1,2,3,4,5}, 10f);
	}
	
	void Update () {
    animationManager.Play("Fly");
	}
}
