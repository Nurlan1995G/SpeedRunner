using System;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI.ProceduralImage;

public class LevelDisplay : MonoBehaviour
{
    [SerializeField] private ProceduralImage _bar;
    [SerializeField] private TMP_Text _label;

    private void Awake()
    {
        _bar.fillAmount = 0;
        OnIndexChanged();
    }

    public void OnEnable()
    {
        Level.ProgressChanged += OnProgressChanged;
        Level.IndexChanged += OnIndexChanged;
    }

    private void OnDisable()
    {
        Level.ProgressChanged -= OnProgressChanged;
        Level.IndexChanged -= OnIndexChanged;
    }

    private void OnProgressChanged(float value) => _bar.fillAmount = value;

    private void OnIndexChanged()
    {
        _label.text = $"{Localization.Translate("Уровень ", "Level ") + Level.CurrentLevelIndex}";
    }
}
