using UnityEngine;
using UnityAtoms.BaseAtoms;
using System;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Adds Variable Instancer's Variable of type System.Int64 to a Collection or List on OnEnable and removes it on OnDestroy. 
    /// </summary>
    [AddComponentMenu("Unity Atoms/Sync Variable Instancer to Collection/Sync Int64 Variable Instancer to Collection")]
    [EditorIcon("atom-icon-delicate")]
    public class SyncInt64VariableInstancerToCollection : SyncVariableInstancerToCollection<System.Int64, Int64Variable, Int64VariableInstancer> { }
}
