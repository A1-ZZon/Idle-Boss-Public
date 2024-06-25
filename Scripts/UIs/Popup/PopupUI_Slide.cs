using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class PopupUI_Slide : PopupUI
{
    private RectTransform rect;
    private float startPos;
    [SerializeField] private float moveDelay;
    [SerializeField] private Button exitBtn;
    
    private void Awake()
    {
        rect = transform.GetChild(0).GetComponent<RectTransform>();
    }

    private void Start()
    {
        exitBtn.onClick.AddListener(UIManager.Instance.CloseUI);

        startPos = rect.sizeDelta.x * -1;
        rect.DOMoveX(0, moveDelay, true).SetAutoKill(true);
    }

    public override void CloseUI()
    {
        rect.DOMoveX(startPos, moveDelay, true)
            .SetAutoKill(true)
            .OnComplete(()=>Destroy(gameObject));
    }
}
