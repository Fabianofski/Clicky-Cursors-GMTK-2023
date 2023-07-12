using UnityEngine;
using UnityAtoms.BaseAtoms;
using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Set variable value Action of type `System.Int64`. Inherits from `SetVariableValue&lt;System.Int64, Int64Pair, Int64Variable, Int64Constant, Int64Reference, Int64Event, Int64PairEvent, Int64VariableInstancer&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-purple")]
    [CreateAssetMenu(menuName = "Unity Atoms/Actions/Set Variable Value/Int64", fileName = "SetInt64VariableValue")]
    public sealed class SetInt64VariableValue : SetVariableValue<
        System.Int64,
        Int64Pair,
        Int64Variable,
        Int64Constant,
        Int64Reference,
        Int64Event,
        Int64PairEvent,
        Int64Int64Function,
        Int64VariableInstancer>
    { }
}
