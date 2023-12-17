using System;

public enum HoldInstruction
{
    s,
    l,
    r,
    f
}

public static class HoldInstructionExtensions
{
    private static readonly Random random = new Random();

    //Get hold instruction that is neither start nor end
    public static HoldInstruction GetRandomHoldInstruction()
    {
        return (HoldInstruction)random.Next(1, 3);
    }

    public static HoldInstruction GetStartHoldInstruction()
    {
        return HoldInstruction.s;
    }

    public static HoldInstruction GetEndHoldInstruction()
    {
        return HoldInstruction.f;
    }
}

