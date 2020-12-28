namespace GRUnlocker {
    public abstract class OptionCore {
        public string desc { get; private set; }
        protected Unlocker unlocker;

        public OptionCore(string desc) {
            this.desc = desc;
            unlocker = new Unlocker();
        }

        public abstract bool Patch(int index = 0);
    }

    public class Option_UnlockAllCollectibles : OptionCore {
        public Option_UnlockAllCollectibles() : base("Unlock all collectibles") { }
        public override bool Patch(int index) => unlocker.UnlockCollectibles();
    }

    public class Option_UnlockAllLevels : OptionCore {
        public Option_UnlockAllLevels() : base("Unlock all levels (classic levels)") { }
        public override bool Patch(int index) => unlocker.UnlockLevels();
    }

    public class Option_UnlockAllLevelsHC : OptionCore {
        public Option_UnlockAllLevelsHC() : base("Unlock all levels (classic + hardcore levels)") { }
        public override bool Patch(int index) => unlocker.UnlockLevelsHC();
    }


    public class Option_UnlockEverything : OptionCore {
        public Option_UnlockEverything() : base("Unlock everything (100%: classic levels)") { }
        public override bool Patch(int index) => unlocker.UnlockAll();
    }

    public class Option_UnlockEverythingHC : OptionCore {
        public Option_UnlockEverythingHC() : base("Unlock everything (100%: classic + hardcore levels)") { }
        public override bool Patch(int index) => unlocker.UnlockAllHC();
    }

    public class Option_UnlockUpToLevel : OptionCore {
        public Option_UnlockUpToLevel() : base("Unlock up to a specific level (1-16) (classic)") { }
        public override bool Patch(int index) => unlocker.UnlockUpToLevel(index);
    }

    public class Option_UnlockUpToLevelHC : OptionCore {
        public Option_UnlockUpToLevelHC() : base("Unlock up to a specific level (1-16) (hardcore)") { }
        public override bool Patch(int index) => unlocker.UnlockUpToLevelHC(index);
    }

    public class Option_NewGameCollectibles : OptionCore {
        public Option_NewGameCollectibles() : base("New-game with collectibles unlocked") { }
        public override bool Patch(int index) => unlocker.NewGameCollectiables();
    }

    public class Option_ResetLevelDetails : OptionCore {
        public Option_ResetLevelDetails() : base("Reset deaths & times") { }
        public override bool Patch(int index) => unlocker.ResetLevelDetails();
    }

    public class Option_ResetCollectibles : OptionCore {
        public Option_ResetCollectibles() : base("Reset collectibles") { }
        public override bool Patch(int index) => unlocker.ResetCollectibles();
    }

    public class Option_ToggleIntros : OptionCore {
        public Option_ToggleIntros() : base("Toggle Intros (Splash screen + Trailer intro)") { }
        public override bool Patch(int index) => unlocker.Game_ToggleIntros();
    }

    public class Option_ReplaceSelectedSword : OptionCore {
        public Option_ReplaceSelectedSword() : base("Replace selected sword") { }
        public override bool Patch(int index) => unlocker.ReplaceSelectedSword(index);
    }

    public class Option_NewGameSword : OptionCore {
        public Option_NewGameSword() : base("New-game with custom sword") { }
        public override bool Patch(int index) => unlocker.NewGameCustomSword(index);
    }


    public class Option_CreateConfig : OptionCore {
        public Option_CreateConfig() : base("Create empty config file(for GRUnlocker)") { }
        public override bool Patch(int index) => unlocker.CreateConfig();
    }

    public class Option_RemoveKevin : OptionCore {
        public Option_RemoveKevin() : base("Remove Kevin (the drone)") { }
        // If you took the time to actually check the code for this, kudos to you!
        // Don't worry, nothing is being changed, just a silly little easteregg.
        public override bool Patch(int index) => true;    // 'LOL NOPE'
    }

    public class Option_WinterDLC : OptionCore {
        public Option_WinterDLC() : base("Unlock Winter DLC (sword+gloves)") { }
        // You really think I'll unlock the dlc for free!?? NO! go buy and support the devs! ($1.99)
        // Don't worry, nothing is being changed.
        public override bool Patch(int index) {
            System.Console.WriteLine("What did you expect? go buy it!\nhttps://store.steampowered.com/app/1480500/Ghostrunner__Winter_Pack/\n");
            return true;
        }
    }
}