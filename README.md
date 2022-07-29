# Rune Factory 5 Library - LocalizedTextHelper

This is not mod. this is library for get localized texts with specific languages for MOD.<br>
Just reference the code or copy it, or reference the released DLL for develop RF5 MOD if it needs.<br>

<img src="https://user-images.githubusercontent.com/17191898/181773072-dc6b7b5d-2940-4a40-9ca5-427ba34dbec6.png" width=500>
(Test logs on runtime)<br>
<br>

If you don't need to get texts outside of the set language, then this library is not needed.<br>
If you having problems of getting specific Ids defined texts, see other repo [here](https://github.com/hisacat/RF5.HisaCat.Lib.TextHelper)

## Useage
First at all, for get other language at runtime, It required prepare (Load localized textdata assets etc...) for use it.<br>
If your mod is not for end-users (ex: mods for dump game datas), normally you can just call ```LocalizedText.PrepareAllSupportedLanguages``` <br>
In other case, if your mod is for end-users, I recommended call prepare function with specific language.<br>

RF5 Supports under languages
* English
* Japanese
* ChineseSimplified
* ChineseTraditional
* Korean
* French
* German

```
//Prepare for item name
{
    bool isSuccess = false;
    LocalizedText.ItemUIName.Prepare(SystemLanguage.Japanese, onSuccess: () => isSuccess = true);
    while (isSuccess == false) yield return null; //Wait until success
}
//Prepare for item description
{
    bool isSuccess = false;
    LocalizedText.ItemUIDiscript.Prepare(SystemLanguage.Japanese, onSuccess: () => isSuccess = true);
    while (isSuccess == false) yield return null; //Wait until success
}
//All just prepare for everything
{
    bool isSuccess = false;
    LocalizedText.PrepareLocalizedTextTypeAllSupportedLanguages(onAllSuccess: () => isSuccess = true);
    while (isSuccess == false) yield return null; //Wait until success
}

//Prints Item_Kyabetsu name, discription in local language
BepInExLoader.log.LogMessage(LocalizedText.ItemUIName.GetText(ItemID.Item_Kyabetsu));
BepInExLoader.log.LogMessage(LocalizedText.ItemUIDiscript.GetText(ItemID.Item_Kyabetsu));

//Prints Item_Kyabetsu name, discription in local japanese
BepInExLoader.log.LogMessage(LocalizedText.ItemUIName.GetText(ItemID.Item_Kyabetsu, SystemLanguage.Japanese));
BepInExLoader.log.LogMessage(LocalizedText.ItemUIDiscript.GetText(ItemID.Item_Kyabetsu, SystemLanguage.Japanese));
```
