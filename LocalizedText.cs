using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace RF5.HisaCat.Lib.LocalizedTextHelper
{
    /// <summary>
    /// Helper class for get localized texts with specific languages in RF5
    /// </summary>
    public static class LocalizedText
    {
        #region Core
        public class NotSupportedLanguageException : Exception
        {
            public NotSupportedLanguageException(string message) : base(message) { }
        }
        public class NotReadyException : Exception
        {
            public NotReadyException(string message) : base(message) { }
        }
        public class RF5DataWasNotLoaded : Exception
        {
            public RF5DataWasNotLoaded(string message) : base(message) { }
        }

        private static void PrepareTextData<T>(int id, SystemLanguage language, Dictionary<SystemLanguage, T> textDic, System.Action onSuccess = null, System.Action<Loader.AssetHandle> onFailed = null) where T : UnityEngine.Object
        {
            if (Loader.AssetManager.IsReady == false)
            {
                onFailed?.Invoke(null);
                return;
            }

            switch (language)
            {
                case SystemLanguage.English:
                case SystemLanguage.Japanese:
                case SystemLanguage.ChineseSimplified:
                case SystemLanguage.ChineseTraditional:
                case SystemLanguage.Korean:
                case SystemLanguage.French:
                case SystemLanguage.German:
                    break;
                default:
                    throw new NotSupportedLanguageException($"Language {language} dose not supported on RF5!. See {nameof(BootOption.SystemLanguage)} for supported language");
            }

            //[NOTE] Remove from HandleList if id already exist for get other languages with same id in same frame
            if (Loader.AssetHandle.HandleList.ContainsKey(id))
                Loader.AssetHandle.HandleList.Remove(id);

            Loader.AssetManager.Entry<T>(id, language, new System.Action<Loader.AssetHandle<T>>((handler) =>
            {
                if (handler != null && handler.IsDone && handler.Result != null)
                {
                    textDic.Add(language, handler.Result);

                    //[NOTE] Remove from HandleList for get other languages with same id later and avoid give effects main game logic.
                    Loader.AssetHandle.HandleList.Remove(id);
                    onSuccess?.Invoke();
                }
                else
                {
                    onFailed?.Invoke(handler);
                }
            }), -1, false);
        }
        #endregion Core

        public enum LocalizedTextType
        {
            ItemUIName,
            ItemUIDiscript,
            UIText,
        }
        public static void PrepareLocalizedTextTypeAllSupportedLanguages(System.Action<LocalizedTextType, SystemLanguage> onSuccess = null, System.Action<LocalizedTextType, SystemLanguage, Loader.AssetHandle> onFailed = null)
        {
            ItemUIName.PrepareAllSupportedLanguages(
                onSuccess: (language) => onSuccess?.Invoke(LocalizedTextType.ItemUIName, language),
                onFailed: (language, handle) => onFailed?.Invoke(LocalizedTextType.ItemUIName, language, handle));
            ItemUIDiscript.PrepareAllSupportedLanguages(
                onSuccess: (language) => onSuccess?.Invoke(LocalizedTextType.ItemUIDiscript, language),
                onFailed: (language, handle) => onFailed?.Invoke(LocalizedTextType.ItemUIDiscript, language, handle));
            UIText.PrepareAllSupportedLanguages(
                onSuccess: (language) => onSuccess?.Invoke(LocalizedTextType.UIText, language),
                onFailed: (language, handle) => onFailed?.Invoke(LocalizedTextType.UIText, language, handle));
        }

        public static class ItemUIName
        {
            internal static Dictionary<SystemLanguage, UISystemTextData> TextDic = null;
            public static void PrepareAllSupportedLanguages(System.Action<SystemLanguage> onSuccess = null, System.Action<SystemLanguage, Loader.AssetHandle> onFailed = null)
            {
                Prepare(SystemLanguage.English,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.English),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.English, r));
                Prepare(SystemLanguage.Japanese,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.Japanese),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.Japanese, r));
                Prepare(SystemLanguage.ChineseSimplified,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.ChineseSimplified),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.ChineseSimplified, r));
                Prepare(SystemLanguage.ChineseTraditional,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.ChineseTraditional),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.ChineseTraditional, r));
                Prepare(SystemLanguage.Korean,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.Korean),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.Korean, r));
                Prepare(SystemLanguage.French,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.French),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.French, r));
                Prepare(SystemLanguage.German,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.German),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.German, r));
            }
            public static void Prepare(SystemLanguage language, System.Action onSuccess = null, System.Action<Loader.AssetHandle> onFailed = null)
            {
                if (TextDic == null)
                    TextDic = new Dictionary<SystemLanguage, UISystemTextData>();
                if (TextDic.ContainsKey(language)) //Already prepared
                {
                    onSuccess?.Invoke();
                    return;
                }

                PrepareTextData((int)Loader.ID.MasterPreload.ITEMUINAMEDATA, language, TextDic, onSuccess, onFailed);
            }
            public static bool IsReady(SystemLanguage language)
            {
                return TextDic != null && TextDic.ContainsKey(language);
            }
            public static bool IsGameDataReady()
            {
                if (SV.UIRes == null) return false;
                return ItemDataTable.Length() > 0;
            }

            public static string GetText(ItemID itemId, SystemLanguage language)
            {
                if (IsGameDataReady() == false)
                    throw new RF5DataWasNotLoaded($"{nameof(ItemUIName)} {typeof(ItemDataTable)} was not loaded yet. please wait for \"{nameof(ItemDataTable.GetDataTables)}\" returns not empty.");

                return GetText(ItemDataTable.GetDataTable(itemId), language);
            }
            public static string GetText(ItemID itemId)
            {
                return GetText(itemId, LanguageManager.GetLanguage());
            }
            public static string GetText(ItemDataTable itemDataTable, SystemLanguage language)
            {
                //Return ScreenName if target language is current language.
                if (language == LanguageManager.GetLanguage()) return itemDataTable.ScreenName;
                return GetText(itemDataTable.ItemIndex, language);
            }
            public static string GetText(ItemDataTable itemDataTable)
            {
                return GetText(itemDataTable, LanguageManager.GetLanguage());
            }
            public static string GetText(int itemIndex)
            {
                return GetText(itemIndex, LanguageManager.GetLanguage());
            }
            public static string GetText(int itemIndex, SystemLanguage language)
            {
                if (IsReady(language) == false)
                    throw new NotReadyException($"{nameof(ItemUIName)} dose not ready. please call \"{nameof(Prepare)}\" first.");

                return TextDic[language].str[itemIndex];
            }
        }

        public static class ItemUIDiscript
        {
            internal static Dictionary<SystemLanguage, UISystemTextData> TextDic = null;
            public static void PrepareAllSupportedLanguages(System.Action<SystemLanguage> onSuccess = null, System.Action<SystemLanguage, Loader.AssetHandle> onFailed = null)
            {
                Prepare(SystemLanguage.English,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.English),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.English, r));
                Prepare(SystemLanguage.Japanese,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.Japanese),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.Japanese, r));
                Prepare(SystemLanguage.ChineseSimplified,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.ChineseSimplified),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.ChineseSimplified, r));
                Prepare(SystemLanguage.ChineseTraditional,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.ChineseTraditional),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.ChineseTraditional, r));
                Prepare(SystemLanguage.Korean,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.Korean),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.Korean, r));
                Prepare(SystemLanguage.French,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.French),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.French, r));
                Prepare(SystemLanguage.German,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.German),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.German, r));
            }
            public static void Prepare(SystemLanguage language, System.Action onSuccess = null, System.Action<Loader.AssetHandle> onFailed = null)
            {
                if (TextDic == null)
                    TextDic = new Dictionary<SystemLanguage, UISystemTextData>();
                if (TextDic.ContainsKey(language)) //Already prepared
                {
                    onSuccess?.Invoke();
                    return;
                }

                PrepareTextData((int)Loader.ID.MasterPreload.ITEMUIDISCRIPTDATA, language, TextDic, onSuccess, onFailed);
            }
            public static bool IsReady(SystemLanguage language)
            {
                return TextDic != null && TextDic.ContainsKey(language);
            }
            public static bool IsGameDataReady()
            {
                if (SV.UIRes == null) return false;
                return ItemDataTable.Length() > 0;
            }

            public static string GetText(ItemID itemId, SystemLanguage language)
            {
                if (IsGameDataReady() == false)
                    throw new RF5DataWasNotLoaded($"{nameof(ItemUIDiscript)} {typeof(ItemDataTable)} was not loaded yet. please wait for \"{nameof(ItemDataTable.GetDataTables)}\" returns not empty.");

                return GetText(ItemDataTable.GetDataTable(itemId), language);
            }
            public static string GetText(ItemID itemId)
            {
                return GetText(itemId, LanguageManager.GetLanguage());
            }
            public static string GetText(ItemDataTable itemDataTable, SystemLanguage language)
            {
                //Return Describe if target language is current language.
                if (language == LanguageManager.GetLanguage()) return itemDataTable.Describe;
                return GetText(itemDataTable.ItemIndex, language);
            }
            public static string GetText(ItemDataTable itemDataTable)
            {
                return GetText(itemDataTable, LanguageManager.GetLanguage());
            }
            public static string GetText(int itemIndex)
            {
                return GetText(itemIndex, LanguageManager.GetLanguage());
            }
            public static string GetText(int itemIndex, SystemLanguage language)
            {
                if (IsReady(language) == false)
                    throw new NotReadyException($"{nameof(ItemUIDiscript)} dose not ready. please call \"{nameof(Prepare)}\" first.");

                return TextDic[language].str[itemIndex];
            }
        }

        public static class UIText
        {
            internal static Dictionary<SystemLanguage, UISystemTextData> TextDic = null;
            public static void PrepareAllSupportedLanguages(System.Action<SystemLanguage> onSuccess = null, System.Action<SystemLanguage, Loader.AssetHandle> onFailed = null)
            {
                Prepare(SystemLanguage.English,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.English),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.English, r));
                Prepare(SystemLanguage.Japanese,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.Japanese),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.Japanese, r));
                Prepare(SystemLanguage.ChineseSimplified,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.ChineseSimplified),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.ChineseSimplified, r));
                Prepare(SystemLanguage.ChineseTraditional,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.ChineseTraditional),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.ChineseTraditional, r));
                Prepare(SystemLanguage.Korean,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.Korean),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.Korean, r));
                Prepare(SystemLanguage.French,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.French),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.French, r));
                Prepare(SystemLanguage.German,
                    onSuccess: () => onSuccess?.Invoke(SystemLanguage.German),
                    onFailed: (r) => onFailed?.Invoke(SystemLanguage.German, r));
            }
            public static void Prepare(SystemLanguage language, System.Action onSuccess = null, System.Action<Loader.AssetHandle> onFailed = null)
            {
                if (TextDic == null)
                    TextDic = new Dictionary<SystemLanguage, UISystemTextData>();
                if (TextDic.ContainsKey(language)) //Already prepared
                {
                    onSuccess?.Invoke();
                    return;
                }

                PrepareTextData((int)Loader.ID.MasterPreload.SYSTEMTEXTDATA, language, TextDic, onSuccess, onFailed);
            }
            public static bool IsReady(SystemLanguage language)
            {
                return TextDic != null && TextDic.ContainsKey(language);
            }
            public static bool IsGameDataReady()
            {
                if (SV.UIRes == null) return false;
                return UITextDic.SystemIdDic != null && UITextDic.SystemIdDic.count > 0;
            }

            public static string GetText(UITextDic.DICID dicid, SystemLanguage language)
            {
                if (IsReady(language) == false)
                    throw new RF5DataWasNotLoaded($"{nameof(UIText)} {typeof(UITextDic)} was not loaded yet. please wait for \"{nameof(UITextDic.SystemIdDic)}\" returns not empty.");

                //Return SV.UIRes.GetSystemText if target language is current language.
                if (language == LanguageManager.GetLanguage()) return SV.UIRes.GetSystemText(dicid);
                return GetText(UITextDic.SystemIdDic[(int)dicid], language);
            }
            public static string GetText(UITextDic.DICID dicid)
            {
                return GetText(dicid, LanguageManager.GetLanguage());
            }
            public static string GetText(int systemId)
            {
                return GetText(systemId, LanguageManager.GetLanguage());
            }
            public static string GetText(int systemId, SystemLanguage language)
            {
                if (IsReady(language) == false)
                    throw new NotReadyException($"{nameof(UIText)} dose not ready. please call \"{nameof(Prepare)}\" first.");

                return TextDic[language].str[systemId];
            }
        }
    }
}
