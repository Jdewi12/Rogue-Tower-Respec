using System;
using System.Reflection;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Respec.Patches
{
    [HarmonyPatch(typeof(UpgradeManager), "ResetUpgrades")]
    internal class Patch_UpgradeManager_ResetUpgrades
    {
        static FieldInfo allButtonsField = typeof(UpgradeManager).GetField("allButtons", BindingFlags.NonPublic | BindingFlags.Instance);
        static FieldInfo xpTextField = typeof(UpgradeManager).GetField("xpText", BindingFlags.NonPublic | BindingFlags.Instance);

        [HarmonyPrefix]
        public static bool Prefix(UpgradeManager __instance)
        {
            try
            {
                var allButtons = (UpgradeButton[])allButtonsField.GetValue(__instance);
                foreach (UpgradeButton upgradeButton in allButtons)
                {
                    if (upgradeButton.enabled)
                    {
                        if (upgradeButton.unlocked)
                        {
                            // we do these after each reset so if there's an error you won't lose xp.
                            __instance.xp += upgradeButton.xpCost;
                            PlayerPrefs.SetInt("XP", __instance.xp);
                        }
                        upgradeButton.ResetUnlock();
                    }
                }
                PlayerPrefs.SetInt("UnlockedCardCount", 0);
                PlayerPrefs.SetInt("Development", 0);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


            }
            catch (Exception e) { throw e; }
            return false;
        }
    }
}
