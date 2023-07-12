using UnityEngine;
using System;
namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// IPair of type `&lt;System.Int64&gt;`. Inherits from `IPair&lt;System.Int64&gt;`.
    /// </summary>
    [Serializable]
    public struct Int64Pair : IPair<System.Int64>
    {
        public System.Int64 Item1 { get => _item1; set => _item1 = value; }
        public System.Int64 Item2 { get => _item2; set => _item2 = value; }

        [SerializeField]
        private System.Int64 _item1;
        [SerializeField]
        private System.Int64 _item2;

        public void Deconstruct(out System.Int64 item1, out System.Int64 item2) { item1 = Item1; item2 = Item2; }
    }
}