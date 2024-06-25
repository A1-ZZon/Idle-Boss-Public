using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace RandomEvent
{
    public class RandomEventResult
    {
        public int GetArrRandomResult(float[] percent)
        {
            float random = GenerateRandomFloat (1f,100f);
            float percentSum = 0;
            int result = 0;

            for (int i = 0; i < percent.Length; i++)
            {
                percentSum += percent[i];
                if (random <= percentSum)
                {
                    result = i;
                    break;
                }
            }
            return result;
        }

        public bool GetBoolRandomResult(float percent)
        {
            float random = GenerateRandomFloat (1f,100f);

            return random <= percent ? true : false;
        }
        
        private float GenerateRandomFloat(float min, float max)
        {
            float randomValue = Random.Range(min, max);
            float roundedValue = Mathf.Round(randomValue * 10f) / 10f; // 소수점 1 번째 자리로 반올림 해줌
            return roundedValue;
        }
    }
}