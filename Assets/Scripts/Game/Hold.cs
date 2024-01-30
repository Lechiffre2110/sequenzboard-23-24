using System;
using System.Collections;
using System.Collections.Generic;

public enum Hold
    {
        A,
        B,
        C,
        D,
        E,
        F,
        G
    }

public static class HoldExtensions
{
    public static readonly int NumberOfHolds = 7;
    
    private static readonly Random random = new Random();
    public static Hold GetRandomHold()
    {
        return (Hold)random.Next(0, NumberOfHolds);
    }

    public static string ToHoldString(this Hold hold)
    {
        return hold.ToString();
    }
}