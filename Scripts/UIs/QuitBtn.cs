using UnityEngine;
using UnityEngine.UI;

public class QuitBtn : MonoBehaviour
{
    private Button quitBtn;

    private void Awake()
    {
        quitBtn = GetComponent<Button>();
    }

    private void Start()
    {
        quitBtn.onClick.RemoveAllListeners();
        quitBtn.onClick.AddListener(QuitGame);
    }

    private void QuitGame()
    {
        Application.Quit();
    }
}
