using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RF5.HisaCat.Lib.LocalizedTextHelper
{
    internal static class Tester
    {
        public static void DoTest()
        {
            //MonsterID.fair
            {
                var lang = UnityEngine.SystemLanguage.Arabic;
                LocalizedText.ItemUIName.Prepare(lang, onSuccess: () =>
                {
                    var text = LocalizedText.ItemUIName.GetText(ItemID.Item_Rabunomidorinku, lang);
                    BepInExLoader.log.LogMessage($"{lang} - {text}");
                }, onFailed: (r) =>
                {
                    BepInExLoader.log.LogMessage($"{lang} - failed");
                });
            }
            return;
            {
                var lang = UnityEngine.SystemLanguage.English;
                LocalizedText.ItemUIName.Prepare(lang, onSuccess: () =>
                {
                    var text = LocalizedText.ItemUIName.GetText(ItemID.Item_Rabunomidorinku, lang);
                    BepInExLoader.log.LogMessage($"{lang} - {text}");
                }, onFailed: (r) =>
                {
                    BepInExLoader.log.LogMessage($"{lang} - failed");
                });
            }

            {
                var lang = UnityEngine.SystemLanguage.Korean;
                LocalizedText.ItemUIDiscript.Prepare(lang, onSuccess: () =>
                {
                    var text = LocalizedText.ItemUIDiscript.GetText(ItemID.Item_Rabunomidorinku, lang);
                    BepInExLoader.log.LogMessage($"{lang} - {text}");
                }, onFailed: (r) =>
                {
                    BepInExLoader.log.LogMessage($"{lang} - failed");
                });
            }
            {
                var lang = UnityEngine.SystemLanguage.English;
                LocalizedText.ItemUIDiscript.Prepare(lang, onSuccess: () =>
                {
                    var text = LocalizedText.ItemUIDiscript.GetText(ItemID.Item_Rabunomidorinku, lang);
                    BepInExLoader.log.LogMessage($"{lang} - {text}");
                }, onFailed: (r) =>
                {
                    BepInExLoader.log.LogMessage($"{lang} - failed");
                });
            }

            {
                var lang = UnityEngine.SystemLanguage.Korean;
                LocalizedText.UIText.Prepare(lang, onSuccess: () =>
                {
                    var text = LocalizedText.UIText.GetText(UITextDic.DICID.ADV_BED_OK, lang);
                    BepInExLoader.log.LogMessage($"{lang} - {text}");
                }, onFailed: (r) =>
                {
                    BepInExLoader.log.LogMessage($"{lang} - failed");
                });
            }
            {
                var lang = UnityEngine.SystemLanguage.English;
                LocalizedText.UIText.Prepare(lang, onSuccess: () =>
                {
                    var text = LocalizedText.UIText.GetText(UITextDic.DICID.ADV_BED_OK, lang);
                    BepInExLoader.log.LogMessage($"{lang} - {text}");
                }, onFailed: (r) =>
                {
                    BepInExLoader.log.LogMessage($"{lang} - failed");
                });
            }
        }
    }
}
