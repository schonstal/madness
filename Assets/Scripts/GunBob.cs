using UnityEngine;
using System.Collections;

public class GunBob : MonoBehaviour {
  public Vector2 magnitude = new Vector2(.003f, .001f);
  public float bobsPerSecond = 2f;
  public GameObject target;
  public float minRunSpeed = 1000f;
  public float returnToNeutralTime = 0.5f;

  Vector3 originalPosition = new Vector3(0f, 0f, 0f);
  float bobTimer = 0f;
  float neutralTimer = 0f;

	void Start () {
	  originalPosition = transform.localPosition;
	}
	
	void Update () {
    if(Mathf.Abs(target.rigidbody.velocity.magnitude) > minRunSpeed) {
      neutralTimer = 0;
      bobTimer += Time.deltaTime * 2 * Mathf.PI * bobsPerSecond;
      transform.localPosition = new Vector3(
          originalPosition.x + magnitude.x * Mathf.Sin(bobTimer),
          originalPosition.y - magnitude.y * Mathf.Abs(Mathf.Cos(bobTimer)),
          originalPosition.z);
    } else {
      bobTimer = 0;
      neutralTimer += Time.deltaTime / returnToNeutralTime;
      transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, neutralTimer);
    }
	}
}
