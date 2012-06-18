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
	  
  bool wasHeld = false;
	
  float originalIntensity;
  float originalRange;
	
  float flashTimer;
  float flashTime = 0.05f;

  void Start() {
    spriteManager = GetComponent<SpriteManager>() as SpriteManager;
    spriteManager.AddAnimation("Fire", new int[] {1,2,3,4,0}, 15f, false);
    spriteManager.AddAnimation("Rest", new int[] {0}, 15f, true);

    originalIntensity = light.intensity;
  }
	
  void Update() {
    flashTimer += Time.deltaTime;
    if(Input.GetButtonDown("Fire1") || Input.GetAxis("Fire1") < 0 && !wasHeld) {
      spriteManager.Play("Fire", true);
      GetComponent<AudioSource>().Play();
      flashTimer = 0;
      light.intensity = flashIntensity;
      light.range = flashRange;
      wasHeld = true;

      RaycastHit hit;
      if(Physics.Raycast(bulletSpawn.position, bulletSpawn.forward, out hit, 300f, layerMask.value)) {
        Debug.DrawLine(transform.position, hit.point, Color.magenta);
        if(hit.transform.CompareTag("Enemy")) {
          squibManager.GetComponent<SparkManager>().SpawnSpark(hit.point);
        } else {
          sparkManager.GetComponent<SparkManager>().SpawnSpark(hit.point);
        }
        hit.collider.BroadcastMessage("ApplyDamage", damage, SendMessageOptions.DontRequireReceiver);
      }
    } else if(flashTimer <= flashTime) {
	  light.intensity = flashIntensity - ((flashTimer/flashTime)*flashIntensity);
    } else {
 	  light.intensity = originalIntensity;
      light.range = originalRange;	
	}
    if(Input.GetAxis("Fire1") == 0) {
      wasHeld = false;
    }
  }
}
