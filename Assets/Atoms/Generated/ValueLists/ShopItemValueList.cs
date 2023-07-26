using UnityEngine;
using F4B1.UI.Shop;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Value List of type `F4B1.UI.Shop.ShopItem`. Inherits from `AtomValueList&lt;F4B1.UI.Shop.ShopItem, ShopItemEvent&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-piglet")]
    [CreateAssetMenu(menuName = "Unity Atoms/Value Lists/ShopItem", fileName = "ShopItemValueList")]
    public sealed class ShopItemValueList : AtomValueList<F4B1.UI.Shop.ShopItem, ShopItemEvent> { }
}
