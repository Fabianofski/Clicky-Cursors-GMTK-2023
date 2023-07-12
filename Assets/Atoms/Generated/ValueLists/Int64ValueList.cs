using UnityEngine;
using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Value List of type `System.Int64`. Inherits from `AtomValueList&lt;System.Int64, Int64Event&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/Int64", fileName = "Int64ValueList")]
    public sealed class Int64ValueList : AtomValueList<System.Int64, Int64Event> { }
}
