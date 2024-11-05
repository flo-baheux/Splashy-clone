using System;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
  [SerializeField] private GameObject platformPrefab;
  [SerializeField] private PlatformSettings defaultPlatformSettings;
  [NonSerialized] public PlatformSettings runtimePlatformSettings;

  float maxLeft = -0.5f, maxRight = 0.5f, maxUp = 0.5f, maxDown = -0.5f, distanceBetween = 1.5f;
  Vector3 lastPlatformPos = Vector3.zero;

  void Awake()
  {
    runtimePlatformSettings = Instantiate(defaultPlatformSettings);
  }

  public void GeneratePlatforms(int nbToGenerate = 1)
  {
    for (int x = 0; x < nbToGenerate; x++)
    {
      Vector3 newPos = new(UnityEngine.Random.Range(maxLeft, maxRight), UnityEngine.Random.Range(maxUp, maxDown), lastPlatformPos.z + distanceBetween);
      Instantiate(platformPrefab, newPos, Quaternion.identity, transform).GetComponent<Platform>().Initialize(runtimePlatformSettings);
      lastPlatformPos = newPos;
    }
  }
}
