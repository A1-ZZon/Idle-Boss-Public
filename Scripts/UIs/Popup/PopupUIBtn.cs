using UnityEngine;
using UnityEngine.UI;

public class PopupUIBtn : MonoBehaviour
{
    private Button button;

    private void Awake()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(OpenUI);
    }

    private void OpenUI()
    {
        UIManager.Instance.OpenUI($"P_{gameObject.name}");
    }
}
