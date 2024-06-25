using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StageInfoUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI stageText;
    private Button stageBtn;

    private void Awake()
    {
        stageBtn = GetComponent<Button>(); 
    }

    private IEnumerator Start()
    {
        yield return null;
        stageBtn.onClick.AddListener(OpenStageUI);
        UpdateStage();
        GameManager.Instance.Stage.OnStageClear += UpdateStage;
    }

    private void OpenStageUI()
    {
        UIManager.Instance.OpenUI($"P_{gameObject.name}");
    }

    private void UpdateStage()
    {
        stageText.text = $"Wave - {GameManager.Instance.Stage.StageNum : 00}";
    }
}
