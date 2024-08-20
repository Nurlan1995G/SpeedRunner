using TMPro;
using UnityEngine;
using System;
using System.Collections;
using Sirenix.OdinInspector;
using System.IO;

public class Chat : MonoBehaviour
{
    [SerializeField] private TMP_Text _values;
    [SerializeField, Min(0)] private int _maxLinesCount;
    [SerializeField] private string[] _messageLinesRu;
    [SerializeField] private string[] _messageLinesEn;
    [SerializeField] private string[] _nameLinesRu;
    [SerializeField] private string[] _nameLinesEn;

    private string[] _currentMessageLines;
    private string[] _currentNameLines;
    private Coroutine _coroutine;

    private const string RuName = "Assets/_ProjectTools/Chat/RuName.txt";
    private const string EnName = "Assets/_ProjectTools/Chat/EnName.txt";
    private const string RuText = "Assets/_ProjectTools/Chat/ChatRu.txt";
    private const string EnText = "Assets/_ProjectTools/Chat/ChatEn.txt";


    [Button]
    private void Initialize()
    {
        _nameLinesRu = File.ReadAllLines(RuName);
        _messageLinesRu = File.ReadAllLines(RuText);

        _nameLinesEn = File.ReadAllLines(EnName);
        _messageLinesEn = File.ReadAllLines(EnText);
    }

    private void OnEnable()
    {
        if (Localization.CurrentLanguage == ".ru")
        {
            _currentNameLines = _nameLinesRu;
            _currentMessageLines = _messageLinesRu;
        }
        else
        {
            _currentNameLines = _nameLinesEn;
            _currentMessageLines = _messageLinesEn;
        }

        _coroutine = StartCoroutine(DrawMessage());
    }

    private void OnDisable()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private IEnumerator DrawMessage()
    {
        int currentLines = 0;

        while (true)
        {
            string name = "<color=#AB00FF>" + _currentNameLines[UnityEngine.Random.Range(0, _currentNameLines.Length)] + ": " + "</color>";
            string message = name + _currentMessageLines[UnityEngine.Random.Range(0, _currentMessageLines.Length)];

            if (currentLines >= _maxLinesCount)
                RemoveFirstLine();

            _values.text = _values.text + Environment.NewLine + message;

            ++currentLines;

            yield return new WaitForSeconds(UnityEngine.Random.Range(5f, 10f));
        }
    }

    private void RemoveFirstLine()
    {
        string[] lines = _values.text.Split('\n');

        if (lines.Length > 1)
        {
            string[] remainingLines = new string[lines.Length - 1];
            Array.Copy(lines, 1, remainingLines, 0, lines.Length - 1);

            _values.text = string.Join(Environment.NewLine, remainingLines);
        }
        else
            _values.text = string.Empty;
    }
}