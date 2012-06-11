using UnityEngine;
using System.Collections;

public class Sprite : MonoBehaviour {
  public SpriteAnimation[] animations;

	void Update () {
  	transform.LookAt(Camera.main.transform);
    transform.rotation = Camera.main.transform.rotation;//Quaternion.Euler(0, transform.eulerAngles.y, 0);
	}
}
