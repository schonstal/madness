using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pistol : MonoBehaviour {
  public Light light;
  public float flashIntensity = 0.25f;
  public float flashRange = 40f;

  SpriteManager spriteManager;
  AudioSource fireSound;

  float originalIntensity;
  float originalRange;

	void Start() {
    spriteManager = GetComponent<SpriteManager>() as SpriteManager;
    spriteManager.AddAnimation("Fire", new int[] {3,1}, 30f, false);

    originalIntensity = light.intensity;
	}
	
	void Update() {
    if(Input.GetButtonDown("Fire1")) {
      spriteManager.Play("Fire", true);
      GetComponent<AudioSource>().Play();
      light.intensity = flashIntensity;
      light.range = flashRange;
    } else {
      light.intensity = originalIntensity;
      light.range = originalRange;
    }
	}
}
