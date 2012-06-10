using UnityEngine;
using System.Collections;

public class SecretDoor : MonoBehaviour {
  void OnDrawGizmos() {
    Gizmos.DrawIcon(transform.position + new Vector3(0f,2.5f,0f), "EditorIcons/Secret.png", true);
  }
}
