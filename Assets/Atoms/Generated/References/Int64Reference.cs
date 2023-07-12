using UnityAtoms.BaseAtoms;
using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Reference of type `System.Int64`. Inherits from `AtomReference&lt;System.Int64, Int64Pair, Int64Constant, Int64Variable, Int64Event, Int64PairEvent, Int64Int64Function, Int64VariableInstancer, AtomCollection, AtomList&gt;`.
    /// </summary>
    [Serializable]
    public sealed class Int64Reference : AtomReference<
        System.Int64,
        Int64Pair,
        Int64Constant,
        Int64Variable,
        Int64Event,
        Int64PairEvent,
        Int64Int64Function,
        Int64VariableInstancer>, IEquatable<Int64Reference>
    {
        public Int64Reference() : base() { }
        public Int64Reference(System.Int64 value) : base(value) { }
        public bool Equals(Int64Reference other) { return base.Equals(other); }
        protected override bool ValueEquals(System.Int64 other)
        {
            throw new NotImplementedException();
        }
    }
}
