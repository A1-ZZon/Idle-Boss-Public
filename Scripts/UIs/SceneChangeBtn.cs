using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeBtn : MonoBehaviour
{
    private Button startBtn;

    private void Awake()
    {
        startBtn = GetComponent<Button>();
    }

    private void Start()
    {
        startBtn.onClick.RemoveAllListeners();
        startBtn.onClick.AddListener(LoadMainScene);
    }

    private void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
