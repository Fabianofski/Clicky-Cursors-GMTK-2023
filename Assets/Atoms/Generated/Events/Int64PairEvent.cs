using UnityEngine;
using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event of type `Int64Pair`. Inherits from `AtomEvent&lt;Int64Pair&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Int64Pair", fileName = "Int64PairEvent")]
    public sealed class Int64PairEvent : AtomEvent<Int64Pair>
    {
    }
}
