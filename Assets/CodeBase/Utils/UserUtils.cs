using System;
using UnityEngine;

namespace Assets.Scripts.Utils
{
    public static class UserUtils
    {
        private static System.Random s_random = new System.Random();

        public static bool GetRandomBool(float successChance) 
        { 
            return s_random.NextDouble() <= successChance;
        }

        public static int GetRandomInt(int minValue = int.MinValue, int maxValue = int.MaxValue)
        { 
            return s_random.Next(minValue, maxValue + 1);
        }

        public static float GetRandomFloat(float minValue = 0, float maxValue = 1) 
        { 
            return (float) s_random.NextDouble() * (maxValue - minValue) + minValue;
        }

        public static int GetRandomSign(float positiveChance = 0.5f) 
        {
            if (GetRandomBool(positiveChance)) 
            {
                return 1;
            }

            return -1;
        }

        public static Vector3 GetRandomVector(Vector3 current, float spreadInnerRadius, float spreadOuterRadius) 
        {
            if (spreadOuterRadius < spreadInnerRadius) 
            {
                throw new ArgumentOutOfRangeException();
            }

            float x = current.x + GetRandomSign() * GetRandomFloat(spreadInnerRadius, spreadOuterRadius);
            float y = current.y + GetRandomInt((int) Math.Round(current.y), (int) Math.Round(spreadOuterRadius));
            float z = current.z + GetRandomSign() * GetRandomFloat(spreadInnerRadius, spreadOuterRadius);

            return new Vector3(x, y, z);
        }
    }
}
