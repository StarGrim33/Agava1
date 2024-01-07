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
            Constants.EnglishCode => Constants.EnglishLanguage,
            Constants.RussianCode => Constants.RussianLanguage,
            Constants.TurkishCode => Constants.TurkishLanguage,
            _ => Constants.RussianLanguage
        };

        LeanLocalization.SetCurrentLanguageAll(language);
    }
}
