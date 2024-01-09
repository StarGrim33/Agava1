using System.Collections;
using Agava.YandexGames;
using Lean.Localization;
using UnityEngine;
using Utils;

namespace Core
{
    public class LanguageInitializer : MonoBehaviour
    {
        private void Start()
        {
            StartCoroutine(InitializeAndChangeLanguage());
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
            string language;

            if (languageCode == Constants.EnglishCode)
            {
                language = Constants.EnglishLanguage;
            }
            else if (languageCode == Constants.RussianCode)
            {
                language = Constants.RussianLanguage;
            }
            else if (languageCode == Constants.TurkishCode)
            {
                language = Constants.TurkishLanguage;
            }
            else
            {
                language = Constants.RussianLanguage;
            }

            LeanLocalization.SetCurrentLanguageAll(language);
        }
    }
}
