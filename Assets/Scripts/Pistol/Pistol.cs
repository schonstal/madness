using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pistol : MonoBehaviour {
  public Light light;
  public float flashIntensity = 0.25f;
  public float flashRange = 40f;
  public float damage = 5f;

  public LayerMask layerMask = -1;

  public Transform bulletSpawn;

  public GameObject prefab;

  SpriteManager spriteManager;
  AudioSource fireSound;
  RaycastHit hit;

  float originalIntensity;
  float originalRange;

	void Start() {
    spriteManager = GetComponent<SpriteManager>() as SpriteManager;
    spriteManager.AddAnimation("Fire", new int[] {1,0}, 30f, false);
    spriteManager.AddAnimation("Rest", new int[] {0}, 30f, true);

    originalIntensity = light.intensity;
	}
	
	void Update() {
    if(Input.GetButtonDown("Fire1")) {
      spriteManager.Play("Fire", true);
      GetComponent<AudioSource>().Play();
      light.intensity = flashIntensity;
      light.range = flashRange;

      transform.localPosition += new Vector3(0, -0.01f, 0);

      if(Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit, 300f, layerMask.value)) {
        Debug.DrawLine(transform.position, hit.point, Color.magenta);
        hit.collider.BroadcastMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
      }
    } else {
      light.intensity = originalIntensity;
      light.range = originalRange;
      spriteManager.Play("Rest");
    }
	}
}
