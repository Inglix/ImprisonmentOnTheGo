using RimWorld;
using Verse;
using Verse.AI;

namespace MakePawnsPrisoners
{
    public class FloatMenuOptionsInjection
    {
        private static bool IsValidDesignationTarget(Thing t)
        {
            var pawn = t as Pawn;
            return pawn?.def != null && !pawn.Dead && pawn.Downed && pawn.RaceProps.Humanlike && pawn.Faction != Faction.OfPlayer;
        }

        private static Job JobOnThing(Pawn pawn, Thing t)
        {
            var pawn2 = t as Pawn;
            if (pawn2 == pawn)
            {
                return null;
            }
            if (pawn2 != null && (pawn2.IsPrisonerOfColony || !pawn.CanReserveAndReach(pawn2, PathEndMode.Touch, Danger.Deadly) || pawn2.Faction == Faction.OfPlayer || !pawn2.RaceProps.Humanlike))
            {
                return null;
            }
            return new Job(ActionDefOf.ImprisonPawn, pawn2);
        }

        public static FloatMenuOption InjectThingFloatOptionIfNeeded(Thing t, Pawn selPawn)
        {
            if (!IsValidDesignationTarget(t))
            {
                return null;
            }
            var pawn = (Pawn)t;
            var job = JobOnThing(selPawn, pawn);
            var floatMenuOption = new FloatMenuOption("Imprison_floatMenu".Translate(pawn.LabelShort), delegate
            {
                selPawn.jobs.TryTakeOrderedJob(job, JobTag.Misc);
            });
            floatMenuOption = FloatMenuUtility.DecoratePrioritizedTask(floatMenuOption, selPawn, pawn);
            if (job == null)
            {
                floatMenuOption.Disabled = true;
            }
            return pawn.IsPrisonerOfColony ? null : floatMenuOption;
        }
    }
}