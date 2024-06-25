using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EnemyDamageText : PoolObject
{
    [Header("Default")]
    [SerializeField] private Sprite defaultIcon;
    [SerializeField] private Material defaultFontMaterial;
    [SerializeField] private float defaultFontSize = 7f;

    [Header("Critical")]
    [SerializeField] private Sprite critIcon;
    [SerializeField] private Material critFontMaterial;

    [Header("Prefab 내부 오브젝트")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI damageText;
    [SerializeField] private float moveDelay;
    [SerializeField] private float targetMove;

    private HorizontalLayoutGroup horizontalLayoutGroup;
    private void Awake()
    {
        horizontalLayoutGroup = GetComponent<HorizontalLayoutGroup>();
    }

    private void OnEnable()
    {
        // Horizontal 오류 방지
        horizontalLayoutGroup.enabled = false;
        horizontalLayoutGroup.enabled = true;
        SetDefaultDamageText();
    }


    public void SetDamageText(int damage, bool isCrit, Transform transform)
    {
        this.transform.position = transform.position + Vector3.up * 15f;
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
        transform.DOMoveY(curY + targetMove, moveDelay, true)
            .SetAutoKill(true)
            .OnComplete(() => gameObject.SetActive(false));
    }
}
