using UnityEngine;
using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Reference Listener of type `Int64Pair`. Inherits from `AtomEventReferenceListener&lt;Int64Pair, Int64PairEvent, Int64PairEventReference, Int64PairUnityEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-orange")]
    [AddComponentMenu("Unity Atoms/Listeners/Int64Pair Event Reference Listener")]
    public sealed class Int64PairEventReferenceListener : AtomEventReferenceListener<
        Int64Pair,
        Int64PairEvent,
        Int64PairEventReference,
        Int64PairUnityEvent>
    { }
}
