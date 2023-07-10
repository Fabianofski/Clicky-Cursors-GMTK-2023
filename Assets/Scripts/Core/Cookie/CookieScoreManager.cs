using F4B1.Audio;
using F4B1.UI;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.Cookie
{
    public class CookieScoreManager : MonoBehaviour
    {
        [Header("Sounds")]
        [SerializeField] private SoundEvent soundEvent;
        [SerializeField] private Sound cookieSound;
        
        [Header("Score Variables")]
        [SerializeField] private IntVariable playerScoreVariable;
        [SerializeField] private IntVariable currentComboVariable;
        [SerializeField] private GameObject scorePopupText;

        public void Click(int score, Vector2 pos)
        {
            var calculatedScore = score * Mathf.Max(1, currentComboVariable.Value);
            playerScoreVariable.Add(calculatedScore);
            soundEvent.Raise(cookieSound);

            var go = Instantiate(scorePopupText, pos, Quaternion.identity);
            go.GetComponentInChildren<ClickScorePopup>().SetNumber(calculatedScore);
        }
    }
}