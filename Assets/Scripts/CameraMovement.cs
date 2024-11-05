using UnityEngine;

public class CameraMovement : MonoBehaviour
{
  [SerializeField] Transform player;
  float initialOffset;

  void Awake()
  {
    initialOffset = transform.position.z;
  }

  void LateUpdate()
  {
    transform.position = new Vector3(player.position.x, transform.position.y, player.position.z + initialOffset);
  }
}
