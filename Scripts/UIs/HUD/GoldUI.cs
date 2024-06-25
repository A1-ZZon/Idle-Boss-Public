using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

public class GoldUI : MonoBehaviour
{
    [SerializeField] private TMP_Text goldAmount;
    [SerializeField] private float changeDelay = 0.5f;
    private int curGold, targetGold;
    private IEnumerator Start()
    {
        yield return null;
        curGold = GameManager.Instance.Player.SaveData.Gold;
        InitGold();
        GameManager.Instance.Player.OnChangeGold += UpdateGold;
    }

    private void InitGold()
    {
        goldAmount.text = $"{curGold}";
    }
    private void UpdateGold()
    {
        targetGold = GameManager.Instance.Player.SaveData.Gold;
        goldAmount.DOCounter(curGold, targetGold, changeDelay, false)
            .SetAutoKill(true)
            .OnComplete(() => { curGold = targetGold; });
    }

    [ContextMenu("GoldUp")]
    private void GoldUp()
    {
        GameManager.Instance.Player.CallChangeGold(10000);
    }
}
