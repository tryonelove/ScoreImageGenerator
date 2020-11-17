
namespace ScoreImageGenerator.Generator.Objects
{
    public enum Mods
    {
        NM = 0,
        NF = 1,
        EZ = 2,
        TD = 4,
        HD = 8,
        HR = 16,
        SD = 32,
        DT = 64,
        RX = 128,
        HT = 256,
        NC = 512, // Only set along with DoubleTime. i.e: NC only gives 576
        FL = 1024,
        AP = 2048,
        SO = 4096,
        RX2 = 8192,    // Autopilot
        PF = 16384, // Only set along with SuddenDeath. i.e: PF only gives 16416  
        FreeModAllowed = NF | EZ | HD | HR | SD | FL | RX | RX2 | SO,
        ScoreIncreaseMods = HD | HR | DT | FL
    }
}