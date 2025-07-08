using System.Collections.Generic;
using RimWorld;
using Verse;
using Verse.AI;
using Verse.Sound;

namespace MakePawnsPrisoners
{
    public class JobDriver_Imprison : JobDriver
    {
        public override bool TryMakePreToilReservations(bool errorOnFailed)
        {
            return true;
        }

        private Pawn Victim => (Pawn)job.GetTarget(TargetIndex.A).Thing;

        protected override IEnumerable<Toil> MakeNewToils()
        {
            this.FailOnDespawnedOrNull(TargetIndex.A);
            this.FailOnNotDowned(TargetIndex.A);
            this.FailOnDespawnedNullOrForbidden(TargetIndex.A);
            yield return Toils_Reserve.Reserve(TargetIndex.A, 1, -1, null);
            var toil = Toils_Goto.GotoThing(TargetIndex.A, PathEndMode.ClosestTouch);
            yield return toil;
            yield return new Toil
            {
                initAction = delegate
                {
                    var thing = new Thing();
                    if (Victim.IsPrisonerOfColony) return;
                    Victim.Faction?.Notify_MemberCaptured(Victim, pawn.Faction);
                    if (job.def != ActionDefOf.ImprisonPawn) return;
                    Victim.guest.CapturedBy(Faction.OfPlayer, pawn);
                    if (MakePawnsPrisoners.settings.PlayShackleSound)
                        ActionDefOf.ShackleClang.PlayOneShot(new TargetInfo(Victim.Position, thing.Map));
                }
            };
            yield return Toils_Reserve.Release(TargetIndex.A);
        }
    }
}