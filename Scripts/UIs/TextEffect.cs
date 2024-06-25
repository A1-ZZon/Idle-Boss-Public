using DG.Tweening;
using TMPro;
using UnityEngine;

public class TextEffect : MonoBehaviour
{
    private TextMeshProUGUI text;

    private void Awake()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        text.DOFade(0f, 0.7f).SetLoops(-1, LoopType.Yoyo);
    }
}
