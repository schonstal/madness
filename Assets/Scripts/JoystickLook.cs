using UnityEngine;
using System.Collections;

public class JoystickLook : MonoBehaviour {
  public float speed = 60f;
	
  void Update() {
    float axisValue = Input.GetAxis("HorizontalRight");
    transform.Rotate(0, axisValue*Mathf.Abs(axisValue)*speed*Time.deltaTime, 0);
  }
}
