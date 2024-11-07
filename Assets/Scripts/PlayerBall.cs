using System;
using DG.Tweening;
using UnityEngine;

public class PlayerBall : MonoBehaviour
{
  [SerializeField] private GameObject platformContainer;
  [SerializeField] private GameObject playerSphere;

  [Header("Movements")]
  [SerializeField] private float maxHeight = 0.8f;
  [SerializeField] private float lateralMoveSpeed = 3f;
  [SerializeField] private float jumpTotalDuration = 0.75f;

  private Vector3 nextPlatformPos;

  public Action OnPlatformHit;
  public Action OnMissedPlatform;

  private bool controlsEnabled = true;

  void Start()
  {
    DOTween.Init();
    Transform firstPlatform = platformContainer.transform.GetChild(0).transform;
    transform.position = firstPlatform.position;
    nextPlatformPos = platformContainer.transform.GetChild(firstPlatform.GetSiblingIndex() + 1).position;
    Jump();
  }

  void Jump()
  {
    transform.DOMoveY(maxHeight, jumpTotalDuration / 1.5f).SetEase(Ease.OutCirc)
    // .OnComplete(() => transform.DOMoveY(nextPlatformPos.y, jumpTotalDuration / 3).SetEase(Ease.InSine));
    .OnComplete(() => transform.DOMoveY(nextPlatformPos.y, jumpTotalDuration / 3).SetEase(Ease.InCirc));

    transform.DOMoveZ(nextPlatformPos.z - 0.2f, jumpTotalDuration).SetEase(Ease.Linear).OnComplete(HandleJumpComplete);
  }

  void Update()
  {
    float horizontalInput = Input.GetAxisRaw("Horizontal");
    if (controlsEnabled && horizontalInput != 0)
      transform.position += lateralMoveSpeed * horizontalInput * Time.deltaTime * Vector3.right;
  }

  void HandleJumpComplete()
  {
    if (TryDetectPlatform(out Platform platform))
    {
      platform.OnHit();
      OnPlatformHit?.Invoke();
      nextPlatformPos = platformContainer.transform.GetChild(platform.transform.GetSiblingIndex() + 1).position;
      Jump();
    }
    else
    {
      controlsEnabled = false;
      OnMissedPlatform?.Invoke();
    }
  }

  private bool TryDetectPlatform(out Platform platform)
  {
    if (Physics.Raycast(playerSphere.transform.position, Vector3.down, out RaycastHit hit, 1f))
    {
      platform = hit.collider.gameObject.GetComponent<Platform>();
      return true;
    }
    platform = null;
    return false;
  }
}
