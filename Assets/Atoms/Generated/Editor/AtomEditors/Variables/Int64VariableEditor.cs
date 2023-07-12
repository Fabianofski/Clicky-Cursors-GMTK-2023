using UnityEditor;
using UnityAtoms.Editor;
using System;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Variable Inspector of type `System.Int64`. Inherits from `AtomVariableEditor`
    /// </summary>
    [CustomEditor(typeof(Int64Variable))]
    public sealed class Int64VariableEditor : AtomVariableEditor<System.Int64, Int64Pair> { }
}
