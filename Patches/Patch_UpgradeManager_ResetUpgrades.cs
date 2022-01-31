
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using HarmonyLib;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

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
				//foreach (UpgradeButton upgradeButton1 in allButtons)
				//	upgradeButton1.CheckEnabled();
				//((Text)xpTextField.GetValue(__instance)).text = "XP: " + __instance.xp.ToString();
				PlayerPrefs.SetInt("UnlockedCardCount", 0);
				PlayerPrefs.SetInt("Development", 0);
				SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);


			}
            catch (Exception e) { throw e; }
            return false;
            
        }
    }
}

