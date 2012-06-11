using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pistol : MonoBehaviour {
  public Light light;
  public float flashIntensity = 0.25f;
  public float flashRange = 40f;
  public float damage = 5f;

  public Transform bulletSpawn;

  public GameObject prefab;

  SpriteManager spriteManager;
  AudioSource fireSound;
  RaycastHit hit;

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

      transform.localPosition += new Vector3(0, -0.01f, 0);

      if(Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit, 300f)) {
        Debug.DrawLine(transform.position, hit.point, Color.magenta);
        hit.collider.BroadcastMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
      }
    } else {
      light.intensity = originalIntensity;
      light.range = originalRange;
    }
	}
}
