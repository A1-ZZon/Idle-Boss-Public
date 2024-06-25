using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BaseStatInfoUI : MonoBehaviour
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI statText;
    [SerializeField] private TextMeshProUGUI statLevelText;
    [SerializeField] private TextMeshProUGUI statDesc;

    private EEnhancementType type;
    private int idx;

    // 초기 1회만 수행
    public void InitStatInfoUI(EEnhancementType type)
    {
        this.type = type;
        idx = (int)this.type;

        // 공통 부분
        List<int> levels = GameManager.Instance.Player.SaveData.GetLevelList();
        List<float> curStat = GameManager.Instance.Player.Stat.GetStatList();
        statLevelText.text = $"Lv.{levels[idx]}";
        statDesc.text = $"+ {curStat[idx]}";

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
}
