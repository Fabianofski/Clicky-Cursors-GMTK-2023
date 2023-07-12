using UnityEngine;
using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Reference Listener of type `System.Int64`. Inherits from `AtomEventReferenceListener&lt;System.Int64, Int64Event, Int64EventReference, Int64UnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Int64 Event Reference Listener")]
    public sealed class Int64EventReferenceListener : AtomEventReferenceListener<
        System.Int64,
        Int64Event,
        Int64EventReference,
        Int64UnityEvent>
    { }
}
