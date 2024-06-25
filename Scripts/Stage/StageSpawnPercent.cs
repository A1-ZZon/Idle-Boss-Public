using System;
using UnityEngine;

[Serializable]
public class StageSpawnPercent
{
    public float[] spawnPercent = new float[3];
    
    public void CalculatePercentage() // 합계가 100%가 되도록 계산해주는 함수
    {
        float perSum = 0f;
        for (int i = 0; i < spawnPercent.Length; i++) 
        {
            if (perSum < 100f)
            {
                if (perSum + spawnPercent[i] >= 100f) // 100퍼센트가 넘는다면
                {
                    spawnPercent[i] = 100f- perSum; // 해당 퍼센트를 100퍼센트가 되도록 맞춰줌
                    perSum = 100f;
                }
                else
                {
                    perSum += spawnPercent[i]; // 더해줌
                }
            }
            else
            {
                spawnPercent[i] = 0; // 합이 100%가 넘을 떈 0으로 고정
            }
        }

        if (perSum < 100f) // 100퍼가 안된다면 맨 마지막 확률이 나머지 퍼센트를 더해줌
        {
            spawnPercent[spawnPercent.Length - 1] += 100 - perSum;
            spawnPercent[spawnPercent.Length - 1] = Mathf.Round(spawnPercent[spawnPercent.Length - 1] * 10) / 10;
        }
    }
}