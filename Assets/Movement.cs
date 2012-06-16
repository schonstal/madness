using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
  public float speed = 10.0f;
  public float maxForward = 10.0f;

  Vector2 velocity = new Vector2(0,0);

  void Update () {
    velocity.x = velocity.y = 0;
    float stickX = Input.GetAxisRaw("HorizontalLeft");
    float stickY = Input.GetAxisRaw("VerticalLeft");
    stickX = stickX * Mathf.Abs(stickX);
    stickY = stickY * Mathf.Abs(stickY);

    velocity.x += Mathf.Clamp(stickX + Input.GetAxisRaw("Horizontal"), -1, 1);
    velocity.y += Mathf.Clamp(stickY + Input.GetAxisRaw("Vertical"), -1, 1);

    Vector3 force = (transform.forward * velocity.y + transform.right * velocity.x) * speed;
    transform.rigidbody.AddForce(force.x, force.y, force.z); 

    transform.rigidbody.velocity = Vector3.ClampMagnitude(transform.rigidbody.velocity, maxForward);
  }
}
