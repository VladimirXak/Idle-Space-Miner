using System.Collections.Generic;
using System;
using UnityEngine;

[Serializable]
public class ListHolder<T>
{
    public List<T> Values;

    public ListHolder()
    {
        Values = new List<T>();
    }
}

