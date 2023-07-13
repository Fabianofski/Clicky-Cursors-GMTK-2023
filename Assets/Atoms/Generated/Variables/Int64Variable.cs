using UnityEngine;
using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Variable of type `System.Int64`. Inherits from `AtomVariable&lt;System.Int64, Int64Pair, Int64Event, Int64PairEvent, Int64Int64Function&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Int64", fileName = "Int64Variable")]
    public sealed class Int64Variable : AtomVariable<long, Int64Pair, Int64Event, Int64PairEvent, Int64Int64Function>
    {
        
        public override object BaseValue
        {
            get => _value;
            set => Value = (int) value;
        }
        
        public void Add(long value)
        {
            SetValue(Value + value, true);
        }

        public void Subtract(long value)
        {
            SetValue(Value - value, true);
        }

        public void SetValue(long newValue)
        {
            SetValue(newValue, true);
        }

        protected override bool ValueEquals(long other)
        {
            return Value == other;
        }
    }
}
