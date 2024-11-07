using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using System;

public class GameManager : MonoBehaviour
{
  [SerializeField] private PlayerBall player;
  [SerializeField] private PlatformManager platformManager;

  [SerializeField] private int nbPlatformHitToIncreaseMultiplier;
  [SerializeField] private List<Color> platformColors;
  private int platformColorIndex = 0;

  private double _score;
  public double Score
  {
    get => _score;
    set
    {
      _score = value;
      OnScoreChange?.Invoke(_score);
    }
  }

  private float _scoreMultiplier = 1;
  public float ScoreMultiplier
  {
    get => _scoreMultiplier;
    set
    {
      _scoreMultiplier = value;
      OnScoreMultiplierChange?.Invoke(_scoreMultiplier);
    }
  }

  private int nbPlatformUntilNextUpgrade;
  public Action<double> OnScoreChange;
  public Action<float> OnScoreMultiplierChange;
  public Action OnGameOver;

  void Start()
  {
    platformManager.GeneratePlatforms(20);
    player.OnPlatformHit += PlatformHitHandler;
    player.OnMissedPlatform += MissedPlatformHandler;
    nbPlatformUntilNextUpgrade = nbPlatformHitToIncreaseMultiplier;
    platformManager.runtimePlatformSettings.material.color = platformColors[platformColorIndex];
  }

  void PlatformHitHandler()
  {
    Camera.main.DOShakeRotation(0.1f, Vector3.forward, 5);
    platformManager.GeneratePlatforms();
    OnHitUpdateScore();
  }

  void OnHitUpdateScore()
  {
    Score += 10 * ScoreMultiplier;
    nbPlatformUntilNextUpgrade--;
    if (nbPlatformUntilNextUpgrade < 0)
    {
      ScoreMultiplier += 0.1f;
      nbPlatformUntilNextUpgrade = nbPlatformHitToIncreaseMultiplier;
      OnMultiplerUpgradeUpdatePlatformsColor();
    }
  }

  void OnMultiplerUpgradeUpdatePlatformsColor()
  {
    platformManager.runtimePlatformSettings.material.
    DOColor(PickNextPlatformColor(), 0.4f)
    .SetEase(Ease.InOutSine);
  }

  private Color PickNextPlatformColor()
  {
    platformColorIndex = (platformColorIndex + 1) % platformColors.Count;
    return platformColors[platformColorIndex];
  }

  void MissedPlatformHandler()
  {
    OnGameOver?.Invoke();
  }
}
