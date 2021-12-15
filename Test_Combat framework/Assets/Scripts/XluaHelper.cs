using System.Collections.Generic;
using UnityEngine;
using XLua;


    [LuaCallCSharp()]
    public static class XluaHelper
    {
        public static Vector2[] GenerateBySelfSpace(int count, float deltaAngle)
        {
            Vector2[] temp=new Vector2[count];
            float halfAngle = (count-1) * deltaAngle / 2;
            float startAngle = 90 - halfAngle;
            for (int i = 0; i < count; i++)
            {
                var a = startAngle + (i) * deltaAngle;
                var x = Mathf.Cos(a * Mathf.Deg2Rad);
                var y = Mathf.Sin(a * Mathf.Deg2Rad);
                temp[i]=new Vector2(x,y);
            }

            return temp;
        }
        public static string[] GenerateBySelfSpaceStr(int count, float deltaAngle)
        {
            string[] temp=new string[count];
            float halfAngle = (count-1) * deltaAngle / 2;
            float startAngle = 90 - halfAngle;
            for (int i = 0; i < count; i++)
            {
                var a = startAngle + (i) * deltaAngle;
                var x = Mathf.Cos(a * Mathf.Deg2Rad);
                var y = Mathf.Sin(a * Mathf.Deg2Rad);
                // temp[i]=new Vector2(x,y);
                temp[i] = $"{x} 0 {y}";
            }

            return temp;
        }
        
        public static Vector2[] Generate(int num)
        {
            Vector2[] temp=new Vector2[num];
            float angle = 0;//起始弧度
            for (int i = 0; i < num ; i++)
            {
                var a = angle + (i) * (360f / num);
                var x = Mathf.Cos(a * Mathf.Deg2Rad);
                var y = Mathf.Sin(a * Mathf.Deg2Rad);
                temp[i]=new Vector2(x,y);
            }

            return temp;
        } 
        public static string[] GenerateStr(int num)
        {
            string[] temp=new string[num];
            float angle = 0;//起始弧度
            for (int i = 0; i < num ; i++)
            {
                var a = angle + (i) * (360f / num);
                var x = Mathf.Cos(a * Mathf.Deg2Rad);
                var y = Mathf.Sin(a * Mathf.Deg2Rad);

                temp[i] = $"{x} 0 {y}";
            }

            return temp;
        } 
    }
