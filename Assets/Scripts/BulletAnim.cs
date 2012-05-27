using UnityEngine;
using System.Collections;

public class BulletAnim : MonoBehaviour {
  IRagePixel ragePixel;
  Animation firedAnimation;

	// Use this for initialization
	void Start () {
    ragePixel = GetComponent<RagePixelSprite>();  
    firedAnimation = new Animation(ragePixel, "Gun", new int[] {1,2,3,4}, 30f);
	}
	
	// Update is called once per frame
	void Update () {
    firedAnimation.Play(Time.deltaTime);
	}
}
