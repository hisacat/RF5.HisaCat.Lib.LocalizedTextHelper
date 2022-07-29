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

                BepInExLoader.log.LogMessage("ItemID.Item_Rabunomidorinku name");
                foreach (var lang in supportLangs)
                    BepInExLoader.log.LogMessage($"  {lang} - {LocalizedText.ItemUIName.GetText(ItemID.Item_Rabunomidorinku, lang)}");
                BepInExLoader.log.LogMessage("\r\n");
                BepInExLoader.log.LogMessage("ItemID.Item_Rabunomidorinku disc");
                foreach (var lang in supportLangs)
                    BepInExLoader.log.LogMessage($"  {lang} - {LocalizedText.ItemUIDiscript.GetText(ItemID.Item_Rabunomidorinku, lang)}");
                BepInExLoader.log.LogMessage("\r\n");

                BepInExLoader.log.LogMessage("DICID.ADV_BED_OK");
                foreach (var lang in supportLangs)
                    BepInExLoader.log.LogMessage($"  {lang} - {LocalizedText.UIText.GetText(UITextDic.DICID.ADV_BED_OK, lang)}");
                BepInExLoader.log.LogMessage("\r\n");

                BepInExLoader.log.LogMessage("MonsterDataID.MD_Ork_01 Name");
                foreach (var lang in supportLangs)
                    BepInExLoader.log.LogMessage($"  {lang} - {LocalizedText.MonsterName.GetText(MonsterDataID.MD_Ork_02, lang)}");
                BepInExLoader.log.LogMessage("\r\n");
                BepInExLoader.log.LogMessage("MonsterDataID.MD_Ork_01 Detail");
                foreach (var lang in supportLangs)
                    BepInExLoader.log.LogMessage($"  {lang} - {LocalizedText.MonsterDetail.GetText(MonsterDataID.MD_Ork_02, lang)}");
                BepInExLoader.log.LogMessage("\r\n");

                BepInExLoader.log.LogMessage("MonsterDataID.MD_Pengi_01 Name");
                foreach (var lang in supportLangs)
                    BepInExLoader.log.LogMessage($"  {lang} - {LocalizedText.MonsterName.GetText(MonsterDataID.MD_Pengi_01, lang)}");
                BepInExLoader.log.LogMessage("\r\n");
                BepInExLoader.log.LogMessage("MonsterDataID.MD_Pengi_01 Detail");
                foreach (var lang in supportLangs)
                    BepInExLoader.log.LogMessage($"  {lang} - {LocalizedText.MonsterDetail.GetText(MonsterDataID.MD_Pengi_01, lang)}");
                BepInExLoader.log.LogMessage("\r\n");
            });
        }
    }
}