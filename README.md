# Demands - Fluently rude run-time assertions

![Build Status](https://ci.appveyor.com/api/projects/status/f6jb5221y6ueo0ho?svg=true)
[NuGet](https://www.nuget.org/packages/Demands/)

Demands is a small library to validate assumptions and yell at the
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

You can demand multiple things:

```C#
Demand.That(foo)
      .AndThat(bar)
      .Because("foo and bar must both be true");
```

You can demand things of lists:

```C#
Demand.ThatFor(someList)
      .AtLeast(42) // AtMost(n) is also supported
      .Match(x => x.IsNotCheese)
      .Because("A certain level of non-cheese content is required");
```

You can throw your own exception:

```C#
Demand.That(foo)
      .OrThrow<SpecialSnowflakeException>("Snowflake wasn't special enough");
```

##What Demands is not

Demands is not a validation library. If you want to do something more
sophisticated than scream and die with your predicate failures, a
package like
[FluentValidation](https://github.com/JeremySkinner/FluentValidation)
might be a better fit.

Demands is also not a contracts framework. You can _use_ it like one,
but that doesn't mean  you _should_. If you want high-powered contracts,
[Code Contracts](http://research.microsoft.com/en-us/projects/contracts/) 
seems to be quite well-regarded.

Demands is _also_ also not a testing framework. I mean, if your code
throws a `DemandUnmetException` in the course of a unit test your test
framework ought to complain, but if you want to make fluent assertions
in your test code you're probably better off with, well [Fluent
Assertions](https://github.com/dennisdoomen/FluentAssertions).

##What Demands is

Demands is a lightweight (I hope), fluent (mostly), and only slightly
snarky package for runtime assertions. It documents assumptions you've
made while writing code, and gives you a platform for shouting at the
exception handler when those assumptions are violated.

Basically, this can make your code a bit more fun to read, your
assumptions a bit more obvious, and your Raygun feed a bit less
perplexing.
