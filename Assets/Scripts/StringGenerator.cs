using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StringGenerator
{
    public static string Generate(BallAbility a)
    {
        switch (a) {
            case BallAbility.BREAK:
                return "Wallhack";
            case BallAbility.JUMP:
                return "Jump";
            case BallAbility.NONE:
                return "Keine";
            case BallAbility.REDIRECT:
                return "Redirect";
            case BallAbility.STOP:
                return "Stop";
        }
        return "";
    }

    public static string Generate(BallBounciness b)
    {
        switch (b) {
            case BallBounciness.SOFT:
                return "hoch";
            case BallBounciness.NORMAL:
                return "normal";
            case BallBounciness.HARD:
                return "niedrig";
        }
        return "";
    }

    public static string Generate(WorldGravity w)
    {
        switch (w) {
            case WorldGravity.LOW:
                return "niedrig";
            case WorldGravity.NORMAL:
                return "normal";
            case WorldGravity.HIGH:
                return "hoch";
        }
        return "";
    }

    public static string Generate(int i)
    {
        return i.ToString();
    }
}
