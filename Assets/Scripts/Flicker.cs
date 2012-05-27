using UnityEngine;
using System.Collections;

public class Flicker : MonoBehaviour {
  AnimationManager animationManager;

	void Start () {
    animationManager = GetComponent<AnimationManager>() as AnimationManager;
    animationManager.AddAnimation("Flicker", new int[] {0,1}, 30f);
	}
	
	void Update () {
    animationManager.Play("Flicker");	
	}
}
