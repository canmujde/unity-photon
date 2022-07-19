using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CM.Tweens
{
    public class DoColorText : MonoBehaviour
    {
        [SerializeField] private Text text;
        [SerializeField] private Color from;
        [SerializeField] private Color to;
        [SerializeField] private float duration;
        [SerializeField] private bool loop;
        [SerializeField] private bool onEnable;
        [SerializeField] private Ease ease;
        


        private void OnEnable()
        {
            if (onEnable)
                Do();
        }

        public void Do()
        {
            text.color = from;
            text.DOColor(to, duration).SetEase(ease).SetLoops(loop ? -1 : 0, LoopType.Yoyo);
        }
    }
}