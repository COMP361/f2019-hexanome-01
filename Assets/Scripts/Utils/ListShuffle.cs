using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {
    public static void Shuffle<T>(this IList<T> list) {
        for (int i = 0; i < list.Count; i++) {
            int randomIndex = Random.Range(i, list.Count);
            T value = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = value;
        }
    }
}