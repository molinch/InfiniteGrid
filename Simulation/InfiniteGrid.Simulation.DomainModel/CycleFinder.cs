using System;
using System.Collections.Generic;
using System.Linq;

namespace InfiniteGrid.Simulation.DomainModel
{
    // TAKEN FROM https://rosettacode.org/wiki/Cycle_detection#C.23
    // AND MODIFIED TO FIT THE EXERCISE

    /// <summary>
    /// Find the length and start of a cycle in a series of objects of any IEquatable type using Brent's cycle algorithm.
    /// </summary>
    public class CycleFinder<T>
    {
        private const int NUMBER_OF_SIMILAR_MOVES_TO_CONSIDER_AS_REPEATING_PATTERN = 50;
        //private static (int, int, IteratorStatus) CannotMoveNext = (0, 0, IteratorStatus.CannotMoveNext);
        private readonly Queue<T> _previous;
        private readonly Func<T, T, bool> _equals;

        public CycleFinder(Func<T, T, bool> equals)
        {
            _previous = new Queue<T>(NUMBER_OF_SIMILAR_MOVES_TO_CONSIDER_AS_REPEATING_PATTERN);
            _equals = equals;
        }

        public void Read(IEnumerable<T> items)
        {
            const int NOT_STARTED = -1;
            _previous.Clear();

            int index = 0;
            int cycleStartIndex = NOT_STARTED;
            int cycleLength = 0;
            foreach (var item in items)
            {
                if (_previous.Count == NUMBER_OF_SIMILAR_MOVES_TO_CONSIDER_AS_REPEATING_PATTERN)
                {
                    bool matched = false;
                    while (_previous.Count > 0 && !matched)
                    {
                        matched = _equals(item, _previous.Dequeue());
                        if (matched)
                        {
                            if (cycleStartIndex == NOT_STARTED)
                            {
                                cycleStartIndex = index - _previous.Count;
                            }
                            else
                            {
                                cycleLength++;
                                if (cycleLength == NUMBER_OF_SIMILAR_MOVES_TO_CONSIDER_AS_REPEATING_PATTERN)
                                {
                                    return;
                                }
                            }
                        }
                        else
                        {
                            cycleStartIndex = NOT_STARTED;
                            cycleLength = 0;
                        }
                    }
                }

                _previous.Enqueue(item);

                index++;
            }
        }

        /*
        public (int Length, int StartIndex, IteratorStatus Status) FindRepeatingCycle(IEnumerator<T> iterator)
        {
            int repeatingCycles = 0;
            (int Length, int StartIndex, IteratorStatus Status) cycle;
            var previousSequence = new List<T>();
            do
            {
                cycle = FindCycle(iterator);
                if (cycle.Status == IteratorStatus.CannotMoveNext)
                {
                    return cycle;
                }

                var sequence = _buffer.GetRange(cycle.StartIndex, cycle.Length);
                if (Enumerable.SequenceEqual(previousSequence, sequence))
                {
                    repeatingCycles++;
                }
                else
                {
                    previousSequence = sequence;
                }
            }
            while (repeatingCycles < NUMBER_OF_CYCLES_TO_CONSIDER_AS_REPEATING_PATTERN);

            return cycle;
        }

        public enum IteratorStatus
        {
            Found,
            CannotMoveNext,
        }

        /// <summary>
        /// Find the cycle length and start position of a series using Brent's cycle algorithm.
        /// 
        ///  Given a recurrence relation X[n+1] = f(X[n]) where f() has
        ///  a finite range, you will eventually repeat a value that you have seen before.
        ///  Once this happens, all subsequent values will form a cycle that begins
        ///  with the first repeated value. The period of that cycle may be of any length.
        /// </summary>
        /// <returns>A tuple where:
        ///    Item1 is lambda (the length of the cycle) 
        ///    Item2 is mu, the zero-based index of the item that started the first cycle.</returns>
        public (int Length, int StartIndex, IteratorStatus Status) FindCycle(IEnumerator<T> iterator)
        {
            _buffer.Clear();

            if (!TryGetNext(iterator, out var first))
            {
                return CannotMoveNext;
            }
            var tortoise = first;

            if (!TryGetNext(iterator, out var hare))
            {
                return CannotMoveNext;
            }

            int power, lambda;
            power = lambda = 1;

            // Find lambda, the cycle length
            while (!_equals(tortoise, hare))
            {
                if (power == lambda)
                {
                    tortoise = hare;
                    power *= 2;
                    lambda = 0;
                }

                if (!TryGetNext(iterator, out hare))
                {
                    return CannotMoveNext;
                }
                
                lambda += 1;
            }

            if (lambda == 0)
            {
                return CannotMoveNext;
            }

            // Find mu, the zero-based index of the start of the cycle
            var hareIndex = lambda;
            var mu = 0;
            tortoise = _buffer[0];
            hare = _buffer[hareIndex];
            while (!_equals(tortoise, hare))
            {
                tortoise = _buffer[mu];
                hare = _buffer[hareIndex + mu];
                mu += 1;
            }

            return (lambda, mu, IteratorStatus.Found);
        }

        private bool TryGetNext(IEnumerator<T> iterator, out T next)
        {
            if (!iterator.MoveNext())
            {
                next = default(T);
                return false;
            }

            next = iterator.Current;
            _buffer.Add(next);
            return true;
        }*/
    }
}