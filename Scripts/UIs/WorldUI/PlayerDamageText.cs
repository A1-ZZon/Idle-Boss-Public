using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerDamageText : PoolObject
{
    [Header("Default")]
    [SerializeField] private Sprite defaultIcon;
    [SerializeField] private Material defaultFontMaterial;
    [SerializeField] private float defaultFontSize;

    [Header("Critical")]
    [SerializeField] private Sprite critIcon;
    [SerializeField] private Material critFontMaterial;

    [Header("Prefab 내부 오브젝트")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private float moveDelay;
    [SerializeField] private float targetMove;
    [SerializeField] private Vector3 uiPosition;

    private HorizontalLayoutGroup horizontalLayoutGroup;
    private RectTransform rect;
    private void Awake()
    {
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
        rect = GetComponent<RectTransform>();
    }

    private void OnEnable()
    {
        horizontalLayoutGroup.enabled = false;
        horizontalLayoutGroup.enabled = true;
        rect.anchoredPosition = uiPosition;
        SetDefaultDamageText();
    }

    public void SetDamageText(int damage, bool isCrit)
    {
        damageText.text = $"{damage}";
        if (isCrit)
        {
            SetCritDamageText();
        }
        MoveText();
    }

    private void SetCritDamageText()
    {
        icon.sprite = critIcon;
        icon.GetComponent<Outline>().enabled = true;
        damageText.fontMaterial = critFontMaterial;
        damageText.fontSize = defaultFontSize + 2f;
    }

    private void SetDefaultDamageText()
    {
        icon.sprite = defaultIcon;
        icon.GetComponent<Outline>().enabled = false;
        damageText.fontMaterial = defaultFontMaterial;
        damageText.fontSize = defaultFontSize;
    }

    private void MoveText()
    {
        float curY = transform.position.y;
        transform.DOMoveY(curY - targetMove, moveDelay, true)
            .SetAutoKill(true)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
