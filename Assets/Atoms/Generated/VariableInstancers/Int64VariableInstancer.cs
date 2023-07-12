using UnityEngine;
using UnityAtoms.BaseAtoms;
using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Variable Instancer of type `System.Int64`. Inherits from `AtomVariableInstancer&lt;Int64Variable, Int64Pair, System.Int64, Int64Event, Int64PairEvent, Int64Int64Function&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-hotpink")]
    [AddComponentMenu("Unity Atoms/Variable Instancers/Int64 Variable Instancer")]
    public class Int64VariableInstancer : AtomVariableInstancer<
        Int64Variable,
        Int64Pair,
        System.Int64,
        Int64Event,
        Int64PairEvent,
        Int64Int64Function>
    { }
}
