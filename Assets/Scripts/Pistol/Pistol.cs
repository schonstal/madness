using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pistol : MonoBehaviour {
  SpriteManager spriteManager;

	void Start() {
    spriteManager = GetComponent<SpriteManager>() as SpriteManager;
    spriteManager.AddAnimation("Fire", new int[] {3,1}, 30f, false);
	}
	
	void Update() {
    if(Input.GetButtonDown("Fire1")) {
      spriteManager.Play("Fire", true);
    }
	}
}
