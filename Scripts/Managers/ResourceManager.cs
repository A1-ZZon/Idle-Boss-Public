using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : Singleton<ResourceManager>
{
    private Dictionary<string, GameObject> uiResources = new Dictionary<string, GameObject>();
    //private ResourcePath resourcePath;
    public GameObject GetUIResource(string name)
    {
        // 이미 한번 Open한 적 있는 친구라면 바로 그냥 오픈
        if (uiResources.ContainsKey(name))
        {
            return uiResources[name];
        }

        // 처음 Open하는 친구라면 Resources에서 오픈
        else
        {
            GameObject go = Resources.Load<GameObject>($"{ResourcePath.UIPath}{name}");
            // 없는 이름일 때에 대한 방어코드는 아직 없어요.
            uiResources.Add(name, go);
            return go;
        }
    }
}
