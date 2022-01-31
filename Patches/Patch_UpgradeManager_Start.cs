
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
    [HarmonyPatch(typeof(UpgradeManager), "Start")]
    internal class Patch_UpgradeManager_Start
    {
		[HarmonyPostfix]
        public static void Postfix(UpgradeManager __instance)
        {
            var resetButton = GameObject.Find("Canvas/UpgradeMenu/ResetButton(DEBUG)").GetComponent<Button>();
            resetButton.onClick = new Button.ButtonClickedEvent();
            resetButton.onClick.AddListener(new UnityEngine.Events.UnityAction(__instance.ResetUpgrades));
            resetButton.GetComponentInChildren<Text>().text = "Respec XP";
        }
    }
}

