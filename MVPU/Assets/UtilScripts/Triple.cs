using UnityEngine;
using System.Collections;

public class Triple<T1, T2,T3>
{
    public T1 first { get; private set; }
    public T2 second { get; private set; }
    public T3 third { get; private set; }
    internal Triple(T1 first, T2 second, T3 third)
    {
        this.first = first;
        this.second = second;
        this.third = third;
    }
}