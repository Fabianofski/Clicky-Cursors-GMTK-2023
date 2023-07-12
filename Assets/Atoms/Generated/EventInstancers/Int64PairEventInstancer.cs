using UnityEngine;
using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Instancer of type `Int64Pair`. Inherits from `AtomEventInstancer&lt;Int64Pair, Int64PairEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-sign-blue")]
    [AddComponentMenu("Unity Atoms/Event Instancers/Int64Pair Event Instancer")]
    public class Int64PairEventInstancer : AtomEventInstancer<Int64Pair, Int64PairEvent> { }
}
