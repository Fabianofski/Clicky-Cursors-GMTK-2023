#if UNITY_2019_1_OR_NEWER
using UnityEditor;
using UnityAtoms.Editor;

namespace UnityAtoms.BaseAtoms.Editor
{
    /// <summary>
    /// Value List property drawer of type `F4B1.UI.Shop.ShopItem`. Inherits from `AtomDrawer&lt;ShopItemValueList&gt;`. Only availble in `UNITY_2019_1_OR_NEWER`.
    /// </summary>
    [CustomPropertyDrawer(typeof(ShopItemValueList))]
    public class ShopItemValueListDrawer : AtomDrawer<ShopItemValueList> { }
}
#endif
