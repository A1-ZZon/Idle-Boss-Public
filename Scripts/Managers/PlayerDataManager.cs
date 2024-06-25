using UnityEngine;
using System.IO;

public class PlayerDataManager : MonoBehaviour
{
    private AESCrypto crypto;
    private string path;

    private void Awake()
    {
        GameManager.Instance.PlayerData = this;
        crypto = new AESCrypto();
        path = Path.Combine(Application.dataPath, "PlayerData.json");
        Debug.Log(path);
    }    

    public void DataSave(PlayerSaveData playerData)
    {
        string json = JsonUtility.ToJson(playerData, true); // 데이터 직렬화
        string encryptedJson = crypto.EncryptString(json); // 직렬화 된 데이터 암호화

        File.WriteAllText(path, encryptedJson);
    }

    public PlayerSaveData DataLoad()
    {
        if (!File.Exists(path))
        {
            Debug.Log("데이터가 존재하지 않습니다 !");
            return new PlayerSaveData(1, 0, 1, 1, 1, 1, 1);
        }

        string encryptedJson = File.ReadAllText(path); // 암호화된 데이터 읽어옴
        string json = crypto.DecryptString(encryptedJson); // 복호화 및 역직렬화

        return JsonUtility.FromJson<PlayerSaveData>(json);
    }
}