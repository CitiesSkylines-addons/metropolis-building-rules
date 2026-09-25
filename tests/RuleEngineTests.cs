using System;
using MetropolisBuildingRules;

public static class RuleEngineTests
{
    private static void Check(bool condition, string name) { if (!condition) throw new Exception(name); }

    public static void Main()
    {
        BuildingCandidate[] pool = {
            new BuildingCandidate("Tower Modern", 90, 0),
            new BuildingCandidate("Midrise European", 45, 1),
            new BuildingCandidate("Lowrise European", 20, 2)
        };
        Check(RuleEngine.Choose(pool, 0, 90, 50, "") == 1, "height fallback");
        Check(RuleEngine.Choose(pool, 1, 45, 50, "european") == 1, "keep vanilla");
        Check(RuleEngine.Choose(pool, 0, 90, 30, "European") == 2, "height and keyword");
        Check(RuleEngine.Choose(pool, 0, 90, 10, "") == 0, "no candidate fallback");
        Check(RuleEngine.Choose(pool, 0, 90, 0, "modern") == 0, "keyword no limit");
        Console.WriteLine("5 building rule checks passed");
    }
}
