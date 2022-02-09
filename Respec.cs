using BepInEx;
using HarmonyLib;

namespace Respec
{
    [BepInPlugin("Jdewi.Respec", "Respec", "1.0.0")]
    public class Respec : BaseUnityPlugin 
    {
        void Awake()
        {
            var instance = new Harmony("Jdewi.Respec");
            instance.PatchAll();
        }
    }
}
