using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

// 차후 switch문이 아닌 script단위로 관리할 수 있도록 설정
public class BaseStatUI : MonoBehaviour
{
    [Header("Stat Object")]
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private TextMeshProUGUI statLevelText;
    [SerializeField] private TextMeshProUGUI statDesc;
    [SerializeField] private TextMeshProUGUI costText;

    [Header("Button Object")]
    [SerializeField] private Button EnhanceBtn;
    [SerializeField] private Image EnhanceImage;
    [SerializeField] private Color DisableBtnColor;
    [SerializeField] private Color EnableBtnColor;
    [SerializeField] private int cost = 100;

    private EEnhancementType type;
    private int idx;
    
    private void Start()
    {
        GameManager.Instance.Player.OnStatLevelUp += UpdateStatUI;
        GameManager.Instance.Player.OnChangeGold += SetButton;
    }

    // 초기 1회만 수행
    public void InitStatUI(EEnhancementType type)
    {
        this.type = type;
        idx = (int)this.type;

        // 공통 부분
        List<int> levels = GameManager.Instance.Player.SaveData.GetLevelList();
        List<float> increases = GameManager.Instance.Player.GetIncreamentList();
        statLevelText.text = $"Lv.{levels[idx]}";
        statDesc.text = $"+ {(increases[idx] * levels[idx])}";
        costText.text = $"{cost}";
        SetButton();

        // 분기 포인트
        switch (type)
        {
            case EEnhancementType.DAMAGE:
                statText.text = "공격력";
                break;

            case EEnhancementType.ATTACKSPEED:
                statText.text = "공격속도";
                break;

            case EEnhancementType.CRITRATE:
                statText.text = "치명타 확률";
                break;

            case EEnhancementType.CRITMULTIPLIER:
                statText.text = "치명타 데미지";
                break;

            case EEnhancementType.HEALTH:
                statText.text = "최대 체력";
                break;
        }
    }

    private void SetButton()
    {
        // 활성화
        if(GameManager.Instance.Player.SaveData.Gold >= cost)
        {
            EnhanceBtn.enabled = true;
            EnhanceBtn.onClick?.RemoveAllListeners();
            EnhanceBtn.onClick.AddListener(LevelUp);
            EnhanceImage.color = EnableBtnColor;
        }
        else
        {
            EnhanceBtn.onClick?.RemoveListener(LevelUp);
            EnhanceBtn.enabled = false;
            EnhanceImage.color = DisableBtnColor;
        }
    }

    // 버튼 누를 때 마다 강화 되는 로직 추가
    public void UpdateStatUI(EEnhancementType type)
    {
        // 공통 부분
        // 실행 시, level Data를 끌어와 셋팅
        List<int> levels = GameManager.Instance.Player.SaveData.GetLevelList();
        List<float> increases = GameManager.Instance.Player.GetIncreamentList();
        statLevelText.text = $"Lv.{levels[idx]}";
        statDesc.text = $"+ {(increases[idx] * levels[idx])}";
    }

    public void LevelUp()
    {
        GameManager.Instance.Player.CallLevelUpEvent((int)type);
        GameManager.Instance.Player.CallChangeGold(-cost);
    }

    private void OnDisable()
    {
        GameManager.Instance.Player.OnStatLevelUp -= UpdateStatUI;
        GameManager.Instance.Player.OnChangeGold -= SetButton;
    }
}
