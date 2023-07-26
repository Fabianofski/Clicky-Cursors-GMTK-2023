#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityEngine.UIElements;
using UnityAtoms.Editor;
using F4B1.UI.Shop;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Event property drawer of type `F4B1.UI.Shop.ShopItem`. Inherits from `AtomEventEditor&lt;F4B1.UI.Shop.ShopItem, ShopItemEvent&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomEditor(typeof(ShopItemEvent))]
    public sealed class ShopItemEventEditor : AtomEventEditor<F4B1.UI.Shop.ShopItem, ShopItemEvent> { }
}
#endif
