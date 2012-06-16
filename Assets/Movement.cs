using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {
  public Vector2 velocity;
  public float speed = 10.0f;
  public float maxForward = 10.0f;
//  public float sensitivity = 0.3;

	// Use this for initialization
	void Start () {
    velocity = new Vector2(0,0);	
	}
	
	// Update is called once per frame
	void Update () {
    velocity.x = velocity.y = 0;	
    velocity.y += Mathf.Floor(Input.GetAxisRaw("Vertical"));
    velocity.x += Mathf.Floor(Input.GetAxisRaw("Horizontal"));

    Vector3 force = (transform.forward * velocity.y + transform.right * velocity.x) * speed;
    transform.rigidbody.AddForce(force.x, force.y, force.z); 

    transform.rigidbody.velocity = Vector3.ClampMagnitude(transform.rigidbody.velocity, maxForward);
  }
}
