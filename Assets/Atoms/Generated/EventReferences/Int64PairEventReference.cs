using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Reference of type `Int64Pair`. Inherits from `AtomEventReference&lt;Int64Pair, Int64Variable, Int64PairEvent, Int64VariableInstancer, Int64PairEventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class Int64PairEventReference : AtomEventReference<
        Int64Pair,
        Int64Variable,
        Int64PairEvent,
        Int64VariableInstancer,
        Int64PairEventInstancer>, IGetEvent 
    { }
}
