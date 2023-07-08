using System;
using System.Collections;
using System.Collections.Generic;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1
{
    public class CookieScoreManager : MonoBehaviour
    {

        [SerializeField] private IntVariable playerScoreVariable;

        public void Click(int score)
        {
            playerScoreVariable.Add(score);
            LeanTween.scale(gameObject, new Vector3(0.9f, 0.9f, 0.9f), 1f).setEasePunch();
        }
        
    }
}
