using System.Collections.Generic;
using UnityEngine;

namespace HakoLibrary.Common
{
    public static class ListExtension
    {
        public static void Shuffle<T>(this IList<T> list)
        {
            int n = list.Count;

            while (n > 1)
            {
                n--;

                int k = Random.Range(0, n);

                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }

        public static T GetRandom<T>(this IList<T> list)
        {
            if (list == null || list.Count == 0)
                throw new System.Exception("IList is null or empty");

            return list[Random.Range(0, list.Count)];
        }

        public static void Destroy<T>(this IList<T> list) where T : Component
        {
            foreach (var item in list)
                Object.Destroy(item.gameObject);

            list.Clear();
        }

        public static void Destroy(this IList<GameObject> list)
        {
            foreach (var item in list)
                Object.Destroy(item.gameObject);

            list.Clear();
        }
    }
}
