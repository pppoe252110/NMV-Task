using System;
using UnityEngine;

public static class RandomExtension
{
    public static int ProceedValue(float random, RandomData[] data)
    {
        var cumulativeSumArray = new float[data.Length];
        cumulativeSumArray[0] = data[0].chance;
        for (int i = 1; i < cumulativeSumArray.Length; ++i)
            cumulativeSumArray[i] = cumulativeSumArray[i - 1] + data[i].chance;
        random = cumulativeSumArray[^1] * random;

        int left = 0;
        int right = cumulativeSumArray.Length - 1;
        while (left < right)
        {
            int mid = (left + right) / 2;
            if (cumulativeSumArray[mid] < random)
                left = mid + 1;
            else
                right = mid;
        }

        return left;
    }

    [Serializable]
    public class RandomData
    {
        [Range(0f, 1f)]
        public float chance = 0.5f;
    }
}