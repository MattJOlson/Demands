# MattUtils.Demand - Fluently rude run-time assertions

Demand is a small library to validate assumptions and yell at the
exception handler if anything unexpected happens.

```C#
public class IntegerInterval
{
    private readonly int _start;
    private readonly int _end;

    public IntegerInterval(int start, int end)
    {
        Demand.That(start <= end)
              .Because("End should not precede start");
        _start = start;
        _end = end;
    }

    // ...
}
```

I wouldn't stake my paycheque on this just yet, but it's probably not
entirely useless.
