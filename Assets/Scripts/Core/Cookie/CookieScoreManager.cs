using F4B1.Audio;
using F4B1.UI;
using UnityAtoms.BaseAtoms;
using UnityEngine;

namespace F4B1.Core.Cookie
{
    public class CookieScoreManager : MonoBehaviour
    {
        [SerializeField] private SoundEvent soundEvent;
        [SerializeField] private Sound cookieSound;
        [SerializeField] private IntVariable playerScoreVariable;
        [SerializeField] private IntVariable currentComboVariable;
        
        [SerializeField] private GameObject scorePopupText;

        public void Click(int score, Vector2 pos)
        {
            var calculatedScore = score * Mathf.Max(1, currentComboVariable.Value);
            playerScoreVariable.Add(calculatedScore);
            soundEvent.Raise(cookieSound);
            LeanTween.scale(gameObject, new Vector3(0.9f, 0.9f, 0.9f), 1f).setEasePunch();

            GameObject go = Instantiate(scorePopupText, pos, Quaternion.identity);
            go.GetComponentInChildren<ClickScorePopup>().SetText(calculatedScore + "");
        }
        
    }
}
