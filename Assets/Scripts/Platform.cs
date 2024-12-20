using DG.Tweening;
using UnityEngine;

public class Platform : MonoBehaviour
{
  [SerializeField] private GameObject platformTop;
  private MeshRenderer platformTopMeshRenderer;

  public void Initialize(PlatformSettings settings)
  {
    platformTopMeshRenderer = platformTop.GetComponentInChildren<MeshRenderer>();
    platformTopMeshRenderer.material = settings.material;
  }

  public void OnHit()
  {
    platformTopMeshRenderer.material.DOColor(Color.magenta, 0.3f);
    transform.DOMoveY(transform.position.y + 0.4f, 0.5f).SetEase(Ease.InOutBack)
    .OnComplete(() => transform.DOMoveY(-5, 0.5f).SetEase(Ease.InQuad));
    Destroy(gameObject, 2f);
  }
}
