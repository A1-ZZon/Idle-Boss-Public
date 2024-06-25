using UnityEngine;

public class P_Enhancement : MonoBehaviour
{
    [SerializeField] private GameObject statPrefab;
    [SerializeField] private Transform contentsTransform;

    private void Start()
    {
        // Count 숫자만큼 statPrefab 생성
        for (int i = 0; i < (int)EEnhancementType.COUNT; i++)
        {
            // 컨텐츠 위치에 statPrefab 생성
            GameObject statObject = Instantiate(statPrefab, contentsTransform);
            statObject.GetComponent<BaseStatUI>().InitStatUI((EEnhancementType)i);
        }
    }
}
