using UnityEngine;
using System.Collections;

public class Spark : MonoBehaviour {
  public float survivalTime = 10f;
  public SparkManager manager = null;
  public GameObject sprite;

  SpriteManager spriteManager;

	void Start() {
    AddAnimations();
	}

  public void Initialize() {
    PlayAnimation();
  }
	
	void Update() {
    if(spriteManager == null) {
      AddAnimations();
    } else if(spriteManager.Finished) {
      active = false;
      killSelf();
    }
	}

  void PlayAnimation() {
    if(spriteManager)
      spriteManager.Play("Sparks"+Mathf.Floor(Random.value * 7.99f), true);
  }

  void AddAnimations() {
    spriteManager = sprite.GetComponent<SpriteManager>() as SpriteManager;
    if(spriteManager) {
      for(int i = 0; i < 8; i++) {
        int spriteFrame = i*2;
        spriteManager.AddAnimation("Sparks"+i, new int[] {spriteFrame,spriteFrame+1}, 15f, false);
      }
      PlayAnimation();
    }
  }

  void killSelf() {
    gameObject.SetActiveRecursively(false);
    if (manager != null)
       manager.push(this);
  }
}
