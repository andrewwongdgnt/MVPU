

public class Quadruple<T1, T2, T3, T4>
{
    public T1 first { get; private set; }
    public T2 second { get; private set; }
    public T3 third { get; private set; }
    public T4 fourth { get; private set; }
    internal Quadruple(T1 first, T2 second, T3 third, T4 fourth)
    {
        this.first = first;
        this.second = second;
        this.third = third;
        this.fourth = fourth;
    }
}