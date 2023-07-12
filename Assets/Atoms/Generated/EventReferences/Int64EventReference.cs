using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event Reference of type `System.Int64`. Inherits from `AtomEventReference&lt;System.Int64, Int64Variable, Int64Event, Int64VariableInstancer, Int64EventInstancer&gt;`.
    /// </summary>
    [Serializable]
    public sealed class Int64EventReference : AtomEventReference<
        System.Int64,
        Int64Variable,
        Int64Event,
        Int64VariableInstancer,
        Int64EventInstancer>, IGetEvent 
    { }
}
