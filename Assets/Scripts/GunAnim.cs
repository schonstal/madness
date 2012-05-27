using UnityEngine;
using System.Collections;

public class GunAnim : MonoBehaviour {
  AnimationManager animationManager;

	void Start () {
    animationManager = GetComponent<AnimationManager>() as AnimationManager;
    animationManager.AddAnimation("Pulse", new int[] {1,2,3,4,0}, 30f, false);
	}
	
	void Update () {
	}

  public void Fire() {
    animationManager.Play("Pulse", true);
  }
}
