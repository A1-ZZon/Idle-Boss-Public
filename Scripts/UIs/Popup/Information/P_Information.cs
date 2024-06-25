using UnityEngine;

public class P_Information : MonoBehaviour
{
    [SerializeField] private GameObject statInfoPrefab;
    [SerializeField] private Transform contentsTransform;

    private void Start()
    {
        // Count 숫자만큼 statPrefab 생성
        for (int i = 0; i < (int)EEnhancementType.COUNT; i++)
        {
            // 컨텐츠 위치에 statPrefab 생성
            GameObject statInfoObject = Instantiate(statInfoPrefab, contentsTransform);
            statInfoObject.GetComponent<BaseStatInfoUI>().InitStatInfoUI((EEnhancementType)i);
        }
    }
}
