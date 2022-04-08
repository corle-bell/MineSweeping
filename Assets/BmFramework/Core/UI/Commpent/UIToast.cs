using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using TMPro;
namespace BmFramework.Core
{
    public class UIToast : MonoBehaviour
    {
        public CanvasGroup group;
        public TextMeshProUGUI text;
        public void Begin(string _text)
        {
            text.text = _text;
            transform.position = new Vector3(Screen.width / 2, -150);

            Sequence seq = DOTween.Sequence();
            seq.Append(transform.DOMoveY(300, 0.8f));
            seq.AppendInterval(0.5f);
            seq.Append(group.DOFade(0, 0.6f)).OnComplete(()=> {
                Destroy(gameObject);
            });
        }
    }
}

