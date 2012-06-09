using UnityEngine;
using System.Collections;

public class GunBob : MonoBehaviour {
  public Vector2 magnitude = new Vector2(.003f, .001f);
  public float cameraBobMagnitude = 0.001f;
  public float bobsPerSecond = 2f;
  public GameObject target;
  public float minRunSpeed = 1000f;
  public float returnToNeutralTime = 0.5f;

  Vector3 originalPosition = new Vector3(0f, 0f, 0f);
  Vector3 cameraOriginalPosition = new Vector3(0f, 0f, 0f);
  float bobTimer = 0f;

  Movement playerMovement;

	void Start () {
	  originalPosition = transform.localPosition;
    cameraOriginalPosition = transform.parent.localPosition;
    playerMovement = target.GetComponent<Movement>();
	}
	
	void Update () {
    bobTimer += Time.deltaTime * 2 * Mathf.PI * bobsPerSecond * 
        (target.rigidbody.velocity.magnitude/playerMovement.maxForward);

    transform.parent.localPosition = new Vector3(
        cameraOriginalPosition.x,
        cameraOriginalPosition.y - cameraBobMagnitude * Mathf.Sin(bobTimer*2),
        cameraOriginalPosition.z);

    transform.localPosition = new Vector3(
        originalPosition.x + magnitude.x * Mathf.Sin(bobTimer),
        originalPosition.y - magnitude.y * Mathf.Abs(Mathf.Cos(bobTimer)),
        originalPosition.z);
	}
}
