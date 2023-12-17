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
        G,
        H,
        I,
        J,
        K,
        L,
        M,
        N,
        O
    }

public static class HoldExtensions
{
    public static readonly int NumberOfHolds = Enum.GetValues(typeof(Hold)).Length;
    
    private static readonly Random random = new Random();
    public static Hold GetRandomHold()
    {
        return (Hold)random.Next(0, NumberOfHolds);
    }
}