using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RF5.HisaCat.Lib.LocalizedTextHelper
{
    internal static class Tester
    {
        public static void DoTest()
        {
            LocalizedText.PrepareLocalizedTextTypeAllSupportedLanguages(onAllSuccess: () =>
            {
                var supportLangs = new SystemLanguage[] {
                    SystemLanguage.English,
                    SystemLanguage.Japanese,
                    SystemLanguage.ChineseSimplified,
                    SystemLanguage.ChineseTraditional,
                    SystemLanguage.Korean,
                    SystemLanguage.French,
                    SystemLanguage.German
                };

                foreach (var lang in supportLangs)
                    BepInExLoader.log.LogMessage($"{lang} - {LocalizedText.ItemUIName.GetText(ItemID.Item_Rabunomidorinku, lang)}");

                foreach (var lang in supportLangs)
                    BepInExLoader.log.LogMessage($"{lang} - {LocalizedText.ItemUIDiscript.GetText(ItemID.Item_Rabunomidorinku, lang)}");

                foreach (var lang in supportLangs)
                    BepInExLoader.log.LogMessage($"{lang} - {LocalizedText.UIText.GetText(UITextDic.DICID.ADV_BED_OK, lang)}");
            });
        }
    }
}