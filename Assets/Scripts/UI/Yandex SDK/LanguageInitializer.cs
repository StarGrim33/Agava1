using Agava.YandexGames;
using Lean.Localization;
using System.Collections;
using UnityEngine;

public class LanguageInitializer : MonoBehaviour
{
    private void Start()
    {
#if UNITY_WEBGL && !UNITY_EDITOR
            StartCoroutine(InitializeAndChangeLanguage());
#endif
    }
    private IEnumerator InitializeAndChangeLanguage()
    {
        while (!YandexGamesSdk.IsInitialized)
        {
            yield return null;
        }

        ChangeLanguage();
    }

    private void ChangeLanguage()
    {
        string languageCode = YandexGamesSdk.Environment.i18n.lang;

        string language = languageCode switch
        {
            Constants.EnglishCode => "English",
            Constants.RussianCode => "Russian",
            Constants.TurkishCode => "Turkey",
            _ => "Russian"
        };

        LeanLocalization.SetCurrentLanguageAll(language);
    }
}
