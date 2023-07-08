using F4B1.Audio;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.Cookie
{
    public class CookieScoreManager : MonoBehaviour
    {
        [SerializeField] private SoundEvent soundEvent;
        [SerializeField] private Sound cookieSound;
        [SerializeField] private IntVariable playerScoreVariable;

        public void Click(int score)
        {
            playerScoreVariable.Add(score);
            soundEvent.Raise(cookieSound);
            LeanTween.scale(gameObject, new Vector3(0.9f, 0.9f, 0.9f), 1f).setEasePunch();
        }
        
    }
}
