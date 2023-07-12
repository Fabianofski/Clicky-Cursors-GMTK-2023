using UnityEngine;
using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event of type `System.Int64`. Inherits from `AtomEvent&lt;System.Int64&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/Int64", fileName = "Int64Event")]
    public sealed class Int64Event : AtomEvent<System.Int64>
    {
    }
}
