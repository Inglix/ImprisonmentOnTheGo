using System.Reflection;
using HarmonyLib;
using Verse;

namespace MakePawnsPrisoners
{
    [StaticConstructorOnStartup]
    internal static class HarmonyPatches
    {
        static HarmonyPatches()
        {
            var harmony = new Harmony("rimworld.agentblac.makepawnsprisoners");
            Harmony.DEBUG = false;
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }
}