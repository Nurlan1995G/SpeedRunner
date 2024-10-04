using UnityEngine;

public static class Localization
{
    private const string RU = ".ru";
    private const string EN = ".en";
    private const string COM = ".com";
    private const string LANGEN = "lang=en";

    public static string CurrentLanguage = RU;

    static Localization()
    {
        if (IsEnglishURL())
            CurrentLanguage = EN;
    }

    private static bool IsEnglishURL()
    {
        return Application.absoluteURL.Contains(COM)
            || Application.absoluteURL.Contains(EN)
            || Application.absoluteURL.Contains(LANGEN);
    }

    public static string Translate(string russian, string english)
    {
        string result = russian;

        if (CurrentLanguage == EN)
            result = english;

        return result;
    }
}