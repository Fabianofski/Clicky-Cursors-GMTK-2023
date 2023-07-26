using UnityEngine;
using F4B1.UI.Shop;

namespace UnityAtoms.BaseAtoms
{
    /// <summary>
    /// Event of type `F4B1.UI.Shop.ShopItem`. Inherits from `AtomEvent&lt;F4B1.UI.Shop.ShopItem&gt;`.
    /// </summary>
    [EditorIcon("atom-icon-cherry")]
    [CreateAssetMenu(menuName = "Unity Atoms/Events/ShopItem", fileName = "ShopItemEvent")]
    public sealed class ShopItemEvent : AtomEvent<F4B1.UI.Shop.ShopItem>
    {
    }
}
