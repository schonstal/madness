using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Pistol : MonoBehaviour {
  public Light light;
  public float flashIntensity = 0.25f;
  public float flashRange = 40f;

  public GunMessage damage;
  public GameObject sparkManager;
  public GameObject squibManager;

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
    spriteManager.AddAnimation("Fire", new int[] {1,2,3,4,0}, 15f, false);
    spriteManager.AddAnimation("Rest", new int[] {0}, 15f, true);

    originalIntensity = light.intensity;
	}
	
	void Update() {
    if(Input.GetButtonDown("Fire1")) {
      spriteManager.Play("Fire", true);
      GetComponent<AudioSource>().Play();
      light.intensity = flashIntensity;
      light.range = flashRange;

      //transform.localPosition += new Vector3(0, -0.01f, 0);

      if(Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit, 300f, layerMask.value)) {
        Debug.DrawLine(transform.position, hit.point, Color.magenta);
        if(hit.transform.tag != "Enemy") {
          sparkManager.GetComponent<SparkManager>().SpawnSpark(hit.point);
        } else {
          squibManager.GetComponent<SparkManager>().SpawnSpark(hit.point);
        }
        hit.collider.BroadcastMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
      }
    } else {
      light.intensity = originalIntensity;
      light.range = originalRange;
      //spriteManager.Play("Rest");
    }
	}
}
