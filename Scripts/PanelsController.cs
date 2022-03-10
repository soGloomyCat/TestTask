using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Events;

public class PanelsController : MonoBehaviour
{
    [SerializeField] private GameObject _startPanel;
    [SerializeField] private GameObject _gameOverPanel;
    [SerializeField] private GameObject _recordsPanel;
    [SerializeField] private Button _startButton;
    [SerializeField] private Button _restartButton;
    [SerializeField] private Button _exitButton;
    [SerializeField] private Button _recordsButton;
    [SerializeField] private Button _closeRecordsButton;
    [SerializeField] private Button _clearButton;
    [SerializeField] private TMP_Text _finalScore;
    [SerializeField] private TMP_InputField _inputField;
    [SerializeField] private ScoreChecker _scoreChecker;
    [SerializeField] private PlayerMover _player;
    [SerializeField] private LeadersChecker _records;

    public string PlayersNick { get; private set; }
    public string PlayersScore { get; private set; }

    public event UnityAction<string, string> GameOver;

    private void OnEnable()
    {
        Time.timeScale = 0;
        _startPanel.SetActive(true);
        _gameOverPanel.SetActive(false);
        _recordsPanel.SetActive(false);
        _player.IsDead += OpenGameOverPanel;
        _startButton.onClick.AddListener(StartGame);
        _restartButton.onClick.AddListener(RestartGame);
        _exitButton.onClick.AddListener(QuitGame);
        _recordsButton.onClick.AddListener(OpenRecordsPanel);
        _closeRecordsButton.onClick.AddListener(CloseRecordsPanel);
        _clearButton.onClick.AddListener(ClearRecords);
    }

    private void OnDisable()
    {
        _player.IsDead -= OpenGameOverPanel;
        _startButton.onClick.RemoveListener(StartGame);
        _restartButton.onClick.RemoveListener(RestartGame);
        _exitButton.onClick.RemoveListener(QuitGame);
        _recordsButton.onClick.RemoveListener(OpenRecordsPanel);
        _closeRecordsButton.onClick.RemoveListener(CloseRecordsPanel);
        _clearButton.onClick.RemoveListener(ClearRecords);
    }

    private void OpenGameOverPanel()
    {
        Time.timeScale = 0;
        _finalScore.text = $"Итоговый счет - {_scoreChecker.Score}";
        _gameOverPanel.SetActive(true);
    }

    private void StartGame()
    {
        _records.LoadData(PlayerPrefs.GetString("Records"));
        Time.timeScale = 3;
        PlayersNick = _inputField.text;
        _startPanel.SetActive(false);
    }

    private void RestartGame()
    {
        PlayerPrefs.SetString("Records", _records.SaveData());
        SceneManager.LoadScene(0);
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void OpenRecordsPanel()
    {
        _recordsPanel.SetActive(true);
        GameOver?.Invoke(PlayersNick, _scoreChecker.Score.ToString());
    }

    private void CloseRecordsPanel()
    {
        _recordsPanel.SetActive(false);
    }

    private void ClearRecords()
    {
        _records.ClearBoard();
    }
}
