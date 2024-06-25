using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour
{
    [SerializeField] private Image hpBar;
    [SerializeField] private TextMeshProUGUI hpBarText;
    [SerializeField] private TextMeshProUGUI hpBarTextFilled;

    private void Start()
    {
        UpdateHealth();
        GameManager.Instance.Player.Health.OnChangeHealth += UpdateHealth;   
    }

    private void UpdateHealth()
    {
        hpBar.fillAmount = GameManager.Instance.Player.Health.GetHealthPercentage();
        ChangeText();
    }

    private void ChangeText()
    {
        hpBarText.text = $"{GameManager.Instance.Player.Health.GetCurHealth()}/{GameManager.Instance.Player.Health.GetMaxHealth()}";
        hpBarTextFilled.text = $"{GameManager.Instance.Player.Health.GetCurHealth()}/{GameManager.Instance.Player.Health.GetMaxHealth()}";
    }
}
