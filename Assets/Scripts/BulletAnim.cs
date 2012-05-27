using UnityEngine;
using System.Collections;

public class BulletAnim : MonoBehaviour {
  AnimationManager animationManager;
	// Use this for initialization
	void Start () {
    animationManager = GetComponent<AnimationManager>() as AnimationManager;
    animationManager.AddAnimation("Pulse", new int[] {1,2,3,4}, 30f);
	}
	
	// Update is called once per frame
	void Update () {
    animationManager.Play("Pulse");
	}
}
