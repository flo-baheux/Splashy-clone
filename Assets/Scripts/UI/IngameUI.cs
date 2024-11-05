using TMPro;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class IngameUI : MonoBehaviour
{
  [SerializeField] private GameManager gameManager;
  [SerializeField] private RectTransform scoreContainer;
  [SerializeField] private Image endGameScorePanel;

  [SerializeField] private TextMeshProUGUI scoreText;
  [SerializeField] private TextMeshProUGUI scoreMultiplierText;

  void Start()
  {
    gameManager.OnScoreChange += OnScoreChangeHandler;
    gameManager.OnScoreMultiplierChange += OnScoreMultiplierChangeHandler;
    gameManager.OnGameOver += OnGameOverHandler;

  }

  void OnScoreChangeHandler(double score)
  {
    scoreText.text = $"{System.Math.Truncate(score)}";
  }

  void OnScoreMultiplierChangeHandler(float scoreMultiplier)
  {
    scoreMultiplierText.text = $"x{scoreMultiplier}";
  }

  void OnGameOverHandler()
  {
    scoreContainer
      .DOAnchorPos(Vector2.zero, 2f)
      .SetEase(Ease.InOutSine)
      .OnComplete(() =>
      {

        DOTween.To(
          () => endGameScorePanel.color.a,
          alpha =>
            endGameScorePanel.color = new Color(endGameScorePanel.color.r, endGameScorePanel.color.g, endGameScorePanel.color.b, alpha),
            0.3f,
            0.2f
        );
      });
  }
}
