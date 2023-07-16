using System;
using UnityEngine;

namespace F4B1.UI
{
    public class Tooltip : MonoBehaviour
    {
        private void OnEnable()
        {
            var gameWasStarted = PlayerPrefs.HasKey("StartedBefore");
            gameObject.SetActive(!gameWasStarted);
            Debug.Log(gameWasStarted);
            PlayerPrefs.SetInt("StartedBefore", 1);
        }
    }
}