using TMPro;
using UnityEngine;

public class TextTranslate : MonoBehaviour
{
    [SerializeField] private string _ruVariant;
    [SerializeField] private string _enVariant;
    [SerializeField] private TMP_Text _text;
    [SerializeField] private Language _language;

    private void OnValidate()
    {
        if (_text == null)
            _text = GetComponent<TMP_Text>();

        if (_text.text != _ruVariant || _text.text != _enVariant)
        {
            switch (_language)
            {
                case Language.Russian:
                    _text.text = _ruVariant;
                    break;
                case Language.English:
                    _text.text = _enVariant;
                    break;
            }
        }
    }

    private void Awake() => _text.text = Localization.Translate(_ruVariant, _enVariant);
}

public enum Language
{
    Russian, English
}