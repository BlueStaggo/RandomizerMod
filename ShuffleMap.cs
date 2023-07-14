using System;
using System.Collections.Generic;
using System.Linq;
using Terraria.Utilities;

namespace RandomizerMod;

public class ShuffleMap<T>
{
    private readonly Dictionary<T, T> map = new();

    public ShuffleMap(UnifiedRandom random, List<T> possibleValues)
    {
        List<T> shuffledValues = possibleValues.OrderBy(_ => random.Next()).ToList();
        for (int i = 0; i < possibleValues.Count; i++)
            map[possibleValues[i]] = shuffledValues[i];
    }

    public T this[T input] => map[input];
}
