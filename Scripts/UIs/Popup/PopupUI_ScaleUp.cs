using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI_ScaleUp : PopupUI
{
    private RectTransform rect;
    [SerializeField] private float scaleUpDelay;
    [SerializeField] private Button exitBtn;
    
    private void Awake()
    {
        rect = transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void Start()
    {
        exitBtn.onClick.AddListener(UIManager.Instance.CloseUI);
        rect.localScale = Vector3.zero;
        rect.DOScale(1f, scaleUpDelay).SetAutoKill(true);
    }

    public override void CloseUI()
    {
        rect.DOScale(0f, scaleUpDelay)
            .SetAutoKill(true)
            .OnComplete(() => Destroy(gameObject));
    }
}