using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreChecker : MonoBehaviour
{
    [SerializeField] private PlayerMover _player;
    [SerializeField] private TMP_Text _scoreText;

    public int Score { get; private set; }

    private void OnEnable()
    {
        Score = 0;
        SetScore();
        _player.ScoreChanged += IncreaseScore;
    }

    private void OnDisable()
    {
        _player.ScoreChanged -= IncreaseScore;
    }

    private void IncreaseScore()
    {
        Score++;
        SetScore();
    }

    private void SetScore()
    {
        _scoreText.text = Score.ToString();
    }
}
