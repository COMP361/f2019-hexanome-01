using System;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethods {
    public static void Shuffle<T>(this IList<T> list) {
        for (int i = 0; i < list.Count; i++) {
            int randomIndex = UnityEngine.Random.Range(i, list.Count);
            T value = list[i];
            list[i] = list[randomIndex];
            list[randomIndex] = value;
        }
    }

    public static Type GetListType<T>(this List<T> _) {
        return typeof(T);
    }

    public static bool IsCompatibleWith(this Type type, Type otherType) {
        return otherType.IsSubclassOf(type)
           || otherType == type;
    }
}
