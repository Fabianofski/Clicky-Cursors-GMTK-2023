using UnityEngine;
using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Variable of type `System.Int64`. Inherits from `AtomVariable&lt;System.Int64, Int64Pair, Int64Event, Int64PairEvent, Int64Int64Function&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-lush")]
    [CreateAssetMenu(menuName = "Unity Atoms/Variables/Int64", fileName = "Int64Variable")]
    public sealed class Int64Variable : AtomVariable<System.Int64, Int64Pair, Int64Event, Int64PairEvent, Int64Int64Function>
    {
        public void Add(System.Int64 value)
        {
            SetValue(Value + value, true);
        }

        public void Subtract(System.Int64 value)
        {
            SetValue(Value - value, true);
        }

        public void SetValue(System.Int64 newValue)
        {
            SetValue(newValue, true);
        }

        protected override bool ValueEquals(System.Int64 other)
        {
            return Value == other;
        }
    }
}
