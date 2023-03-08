using static UnityEngine.Mathf;
using UnityEngine;
using UnityEngine.UIElements;

public static class FunctionLibrary
{
    public delegate float Function(float x, float t);
    public static float Wave(float position, float time) => Sin(PI * (position + time));

    public static float MultiWave(float position, float time)
    {
        float y = Sin(PI * (position + 0.5f * time));
        y += 0.5f * Sin(2f * PI * (position + time));
        return y * (2f / 3f);
    }
    public static float Ripple(float position, float time) => Sin(PI * (4f * Abs(position) - time)) / (1f + 10f * position * position);

    public static Function GetFunction(int index)
    {
        if (index == 0)
        {
            return Wave;
        }
        else if (index == 1)
        {
            return MultiWave;
        }
        else
        {
            return Ripple;
        }
    }
}
