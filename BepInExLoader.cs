using BepInEx;
using UnhollowerRuntimeLib;
using HarmonyLib;

namespace RF5.HisaCat.Lib.LocalizedTextHelper
{

    [BepInPlugin(GUID, MODNAME, VERSION)]
    public class BepInExLoader : BepInEx.IL2CPP.BasePlugin
    {
        public const string
            MODNAME = "Lib.LocalizedTextHelper",
            AUTHOR = "HisaCat",
            GUID = "com." + AUTHOR + "." + MODNAME,
            VERSION = "1.0.0.0";

        public static BepInEx.Logging.ManualLogSource log;
        public BepInExLoader()
        {
            log = Log;
        }

        public override void Load()
        {
            try
            {
                Harmony.CreateAndPatchAll(typeof(AssetManagerHooker));
            }
            catch
            {
                log.LogError($"[{GUID}] FAILED to Register Il2Cpp Types!");
            }
        }

        [HarmonyPatch]
        public class AssetManagerHooker
        {
            private static bool IsInitialized = false;
            [HarmonyPatch(typeof(Loader.AssetManager), nameof(Loader.AssetManager.Update))]
            [HarmonyPostfix]
            public static void UpdatePostfix(Loader.AssetManager __instance)
            {
                if (IsInitialized) return;
                if (Loader.AssetManager.IsReady == false) return;

                if (LocalizedText.ItemUIName.IsGameDataReady() == false) return;
                if (LocalizedText.ItemUIDiscript.IsGameDataReady() == false) return;
                if (LocalizedText.UIText.IsGameDataReady() == false) return;

                IsInitialized = true;

                Tester.DoTest();
            }
        }
    }
}
