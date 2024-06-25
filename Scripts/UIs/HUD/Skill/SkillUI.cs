using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
public class SkillUI : MonoBehaviour
{
    [SerializeField] private GameObject coolDownBG;
    private Image coolDownBGImage;

    private void Awake()
    {
        coolDownBGImage = coolDownBG.GetComponent<Image>();
    }

    public void ActiveCoolDown(float coolTime)
    {
        coolDownBG.SetActive(true);
        coolDownBGImage.DOFillAmount(0, coolTime)
            .SetAutoKill(true)
            .OnComplete(() =>
            {
                coolDownBGImage.fillAmount = 1;
                coolDownBG.SetActive(false);
            });
    }
}