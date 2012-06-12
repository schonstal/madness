using UnityEngine;
using System.Collections;

public class SpitterBrain : MonoBehaviour {
  public GameObject shooterTarget;

  public float cringeTime = 0.2f;
  public float hitPoints = 15;

  public int fireIterationMin = 1;
  public int fireIterationMax = 3;

  public float fallAmount = 1.2f;
  public float fallTime = 0.25f;

  SpitBallShooter shooter;
  SpriteManager animationManager;

  Vector3 fallPosition;
  Vector3 oldPosition;

  bool hurt = false;
  bool wasHurt = false;
  bool dead = false;

  float hurtTimer = 0;

	void Start () {
    renderer.material.color = new Color(1,0,1);
    shooter = shooterTarget.GetComponent<SpitBallShooter>() as SpitBallShooter; 

    animationManager = GetComponent<SpriteManager>() as SpriteManager;

    animationManager.AddAnimation("Fly", new int[] {0,1,2,3,4,5}, 9f);
    animationManager.AddAnimation("Fire", new int[] {7,8,9,10,11,12}, 9f);
    fireIterationMax += 1;

    animationManager.AddAnimation("Die", new int[] {13,6}, 15f, false);
    animationManager.AddAnimation("Hurt", new int[] {13}, 9f);

    animationManager.Play("Fly");
	}
	
	void Update () {
    animationManager = GetComponent<SpriteManager>() as SpriteManager;
    if(hitPoints <= 0) {
      animationManager.Play("Die");
      hurtTimer += Time.deltaTime/fallTime;
      transform.position = Vector3.Lerp(oldPosition, fallPosition, hurtTimer);
    } else if(hurt) {
      hurtTimer += Time.deltaTime;
      if(hurtTimer >= cringeTime && hitPoints > 0) {
        hurt = false;
        hurtTimer = 0;
      }
      animationManager.Play("Hurt");
      wasHurt = true;
    } else {
//        shooter.Fire();
      animationManager.Play("Fly",(wasHurt ? true : false));
      wasHurt = false;
    }

	}

  public void ApplyDamage(float damage) {
    if(!dead) {
      Debug.Log(hitPoints);
      hurtTimer = 0;
      hitPoints -= damage;
      hurt = true;

      if(hitPoints <= 0) {
        fallPosition = transform.position - new Vector3(0,fallAmount,0);
        oldPosition = transform.position;
        dead = true;
        collider.isTrigger = true;
      }
    }
  }
}
