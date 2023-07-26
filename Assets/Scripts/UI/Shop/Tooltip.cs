using UnityEngine;

namespace F4B1.UI.Shop
{
    public class Tooltip : MonoBehaviour
    {
        private void OnEnable()
        {
            var gameWasStarted = PlayerPrefs.HasKey("StartedBefore");
            gameObject.SetActive(!gameWasStarted);
            PlayerPrefs.SetInt("StartedBefore", 1);
        }
    }
}