using System;
using UnityEngine;

public class RandomManager : Yube.Singleton<RandomManager>
{
    private void Start()
    {
        seed = Guid.NewGuid().GetHashCode();
        random = new System.Random(seed);
        Debug.Log("Seed: " + seed);
    }

    public int GetRandomInt(int min, int max, bool maxIncluded = true)
    {
        return random.Next(min, maxIncluded ? max + 1 : max);
    }

    public float GetRandomFloat01()
    {
        return (float)random.NextDouble();
    }

    public float GetRandomFloat(float min, float max)
    {
        float range = max - min;
        float randNumber = ((float)random.NextDouble() * range) + min;
        return randNumber;
    }

    private System.Random random = null;
    private int seed = 0;
}
