using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class LeadersChecker : MonoBehaviour
{
    [SerializeField] private List<TMP_Text> _leaders;
    [SerializeField] private PanelsController _panelsController;

    private void OnEnable()
    {
        _panelsController.GameOver += ChangeLeadersBoard;
    }

    private void OnDisable()
    {
        _panelsController.GameOver -= ChangeLeadersBoard;
    }

    public string SaveData()
    {
        string text = "";

        for (int i = 0; i < _leaders.Count; i++)
        {
            text += _leaders[i].text + ",";
        }

        return text;
    }

    public void LoadData(string text)
    {
        string[] texts;

        if (text != null)
        {
            texts = new string[_leaders.Count];
            texts = text.Split(',');

            for (int i = 1; i < texts.Length; i++)
            {
                _leaders[i - 1].text = texts[i - 1];
            }
        }
    }

    public void ClearBoard()
    {
        PlayerPrefs.DeleteAll();

        for (int i = 0; i < _leaders.Count; i++)
        {
            _leaders[i].text = "";
        }
    }

    private void ChangeLeadersBoard(string name = "", string score = "")
    {
        for (int i = _leaders.Count - 1; i > 0; i--)
        {
            _leaders[i].text = _leaders[i - 1].text;
        }

        _leaders[0].text = $"{name} - {score}";
    }
}
