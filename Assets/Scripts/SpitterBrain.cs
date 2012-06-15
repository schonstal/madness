using UnityEngine;
using System.Collections;

public class SpitterBrain : MonoBehaviour {
  public GameObject shooterTarget;

  public float cringeTime = 0.2f;
  public float hitPoints = 15;

  public float minFireTime = 0.2f;
  public float maxFireTime = 0.4f;
  public float fireTime;

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

  float fireTimer = 0;

	void Start () {
    renderer.material.color = new Color(1,0,1);
    shooter = shooterTarget.GetComponent<SpitBallShooter>() as SpitBallShooter; 

    animationManager = GetComponent<SpriteManager>() as SpriteManager;

    animationManager.AddAnimation("Fly", new int[] {0,1,2,3,4,5}, 10f);
    animationManager.AddAnimation("Fire", new int[] {18,19,20,21,22,23}, 10f);
    fireIterationMax += 1;

    animationManager.AddAnimation("Die", new int[] {8,9,10,11,12,13,14,15,16,17,16}, 15f, false);
    animationManager.AddAnimation("Hurt", new int[] {13,14}, 10f, false);

    animationManager.Play("Fly");

    fireTime = Random.Range(minFireTime, maxFireTime);
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
//      animationManager.Play("Hurt", true);
      wasHurt = true;
    } else {
      fireTimer += Time.deltaTime;
      if(fireTimer >= fireTime) { 
        fireTime = Random.Range(minFireTime, maxFireTime);
        shooter.Fire();
        fireTimer = 0;
        int lastFrame = animationManager.CurrentAnimation.FrameIndex;
        animationManager.Play("Fire", false);
        animationManager.CurrentAnimation.frameIndex = lastFrame;
      }
      animationManager.Play("Fly",(wasHurt ? true : false));
      wasHurt = false;
    }

	}

  public void ApplyDamage(GunMessage message) {
    if(!dead) {
      Debug.Log(hitPoints);
      hurtTimer = 0;
      hitPoints -= message.damage;
      hurt = true;
      transform.parent.position += transform.parent.forward * message.knockBack;
      animationManager.Play("Hurt", true);

      if(hitPoints <= 0) {
        fallPosition = transform.position - new Vector3(0,fallAmount,0);
        oldPosition = transform.position;
        dead = true;
        collider.isTrigger = true;
      }
    }
  }
}
