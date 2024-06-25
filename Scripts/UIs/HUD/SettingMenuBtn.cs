using UnityEngine;
using UnityEngine.UI;

public class SettingMenuBtn : MonoBehaviour
{
    private Button menuBtn;

    private void Awake()
    {
        menuBtn = GetComponent<Button>();
    }

    private void Start()
    {
        menuBtn.onClick.RemoveAllListeners();
        menuBtn.onClick.AddListener(OpenSettingMenu);
    }

    private void OpenSettingMenu()
    {
        UIManager.Instance.OpenUI($"P_{gameObject.name}");
    }
}
