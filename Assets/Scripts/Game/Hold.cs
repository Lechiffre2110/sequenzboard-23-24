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
    
    /// <summary>
    /// Gets a random hold.
    /// </summary>
    /// <returns>A random hold.</returns>
    public static Hold GetRandomHold()
    {
        return (Hold)random.Next(0, NumberOfHolds);
    }

    /// <summary>
    /// Converts a Hold to a string.
    /// </summary>
    /// <returns>A string representing the hold.</returns>
    public static string ToHoldString(this Hold hold)
    {
        return hold.ToString();
    }
}