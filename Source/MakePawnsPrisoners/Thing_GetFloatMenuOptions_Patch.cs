using System.Collections.Generic;
using System.Linq;
using HarmonyLib;
using Verse;

namespace MakePawnsPrisoners
{
    [HarmonyPatch(typeof(ThingWithComps), "GetFloatMenuOptions")]
    internal class Thing_GetFloatMenuOptions_Patch
    {
        [HarmonyPostfix]
        public static void GetFloatMenuOption(ref IEnumerable<FloatMenuOption> __result, Thing __instance, Pawn selPawn)
        {
            var floatMenuOption = FloatMenuOptionsInjection.InjectThingFloatOptionIfNeeded(__instance, selPawn);
            if (floatMenuOption == null) return;
            var list = __result.ToList<FloatMenuOption>();
            list.Add(floatMenuOption);
            __result = list;
        }
    }
}