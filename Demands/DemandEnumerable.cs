using System;
using System.Collections.Generic;
using System.Linq;

namespace Demands
{
    public class DemandEnumerable<T>
    {
        private readonly IEnumerable<T> _seq;

        public DemandEnumerable(IEnumerable<T> seq)
        {
            _seq = seq;
        }

        public DemandEnumerableCounter<T> AtMost(int n)
        {
            return new DemandEnumerableCounter<T>(_seq, m => n < m);
        }

        public DemandEnumerableCounter<T> AtLeast(int n)
        {
            return new DemandEnumerableCounter<T>(_seq, m => m < n);
        }
    }

    public class DemandEnumerableCounter<T>
    {
        private IEnumerable<T> _seq;
        private Func<int, bool> _throwIf; 

        public DemandEnumerableCounter(IEnumerable<T> seq, Func<int, bool> throwIf)
        {
            _seq = seq;
            _throwIf = throwIf;
        }

        public DemandEnumerableEvaluator<T> Match(Func<T, bool> predicate)
        {
            Action<IEnumerable<T>, Func<IEnumerable<T>, string>> tester = (seq, msgfunc) => {
                var matches = seq.Where(predicate);
                if (_throwIf(matches.Count())) {
                    throw new DemandUnmetException(msgfunc(matches));
                }
            };
            return new DemandEnumerableEvaluator<T>(_seq, tester);
        }
    }

    public class DemandEnumerableEvaluator<T>
    {
        private readonly Action<IEnumerable<T>, Func<IEnumerable<T>, string>> _tester;
        private readonly IEnumerable<T> _seq;

        public DemandEnumerableEvaluator(IEnumerable<T> seq, Action<IEnumerable<T>, Func<IEnumerable<T>, string>> tester)
        {
            _seq = seq;
            _tester = tester;
        }

        public void Because(string reason) => Because(matches => reason);

        public void Because(Func<IEnumerable<T>, string> msgfunc) => _tester(_seq, msgfunc);
    }
}