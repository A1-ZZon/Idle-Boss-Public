using UnityEngine;

public class UIManager : Singleton<UIManager>
{
    private Transform popupUiTransform;
    private GameObject curUI;

    protected override void Awake()
    {
        base.Awake();
        popupUiTransform = GameObject.Find(ScenePath.PopupUIPath).transform;
    }

    public void OpenUI(string name)
    {
        // 이미 로드 되어 있는 경우 더 open하지 않는 방어 코드 필요
        GameObject newUI = ResourceManager.Instance.GetUIResource(name);
        if(curUI == null)
            curUI = Instantiate(newUI, popupUiTransform);
        else
        {
            // 두 UI의 tag가 같으면 Open 더 이상 먹지 않게 셋팅
            if (curUI.CompareTag(newUI.tag)) 
            {
                CloseUI();    
                return; 
            }

            CloseUI();
            curUI = null;
            OpenUI(name);
        }
    }
    // 단일 PopupUI를 끄는 함수
    public void CloseUI()
    {
        curUI.GetComponent<PopupUI>().CloseUI();
    }

    // Health의 TakeDamage에서 호출 
    public void CallEnemyDamageUI(int damage, bool isCrit, Transform position)
    {
        EnemyDamageText damageText = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.EnemyDamageText).ReturnMyComponent<EnemyDamageText>();
        damageText.SetDamageText(damage, isCrit, position);
    }

    public void CallPlayerDamageUI(int damage, bool isCrit)
    {
        PlayerDamageText damageText = GameManager.Instance.Pool.SpawnFromPool(EPoolObjectType.PlayerDamageText).ReturnMyComponent<PlayerDamageText>();
        damageText.SetDamageText(damage,isCrit);
    }
}
