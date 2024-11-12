using UnityEngine;
using Verse;

namespace MakePawnsPrisoners
{
    public class MakePawnsPrisoners : Mod
    {
        public static Settings settings;
        
        public MakePawnsPrisoners(ModContentPack modContentPack) : base(modContentPack)
        {
            settings = GetSettings<Settings>();
        }

        public override void DoSettingsWindowContents(Rect canvas)
        {
            Listing_Standard listingStandard = new Listing_Standard();
            listingStandard.Begin(canvas);
            listingStandard.CheckboxLabeled("Inglix.PlayShackleSound".Translate(), ref settings.PlayShackleSound);
            listingStandard.End();
            base.DoSettingsWindowContents(canvas);
        }

        public override string SettingsCategory()
        {
            return "Inglix.MakePawnsPrisoners".Translate();
        }
    }

    public class Settings : ModSettings
    {
        public bool PlayShackleSound = true;

        public override void ExposeData()
        {
            Scribe_Values.Look(ref PlayShackleSound, "playShackleSound", true);
            base.ExposeData();
        }
    }
}