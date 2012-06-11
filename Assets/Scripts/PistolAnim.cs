using UnityEngine;
using System.Collections;

public class PistolAnim : MonoBehaviour {
  SpriteManager spriteManager;

	void Start () {
    spriteManager = GetComponent<SpriteManager>() as SpriteManager;
    spriteManager.AddAnimation("Fire", new int[] {2}, 30f, false);
	}
	
	void Update () {
	}

  public void Fire() {
    spriteManager.Play("Fire", true);
  }
}
