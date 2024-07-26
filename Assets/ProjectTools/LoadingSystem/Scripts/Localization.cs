using UnityEngine;

public static class Localization
{
    private const string RU = ".ru";
    private const string EN = ".en";
    private const string COM = ".com";

    public static string CurrentLanguage = RU;

    static Localization()
    {
        if (Application.absoluteURL.Contains(COM) || Application.absoluteURL.Contains(EN))
            CurrentLanguage = EN;
    }

    public static string Translate(string russian, string english)
    {
        string result = russian;

        if (CurrentLanguage == EN)
            result = english;

        return result;
    }
}