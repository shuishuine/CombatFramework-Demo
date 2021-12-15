using BehaviorDesigner.Runtime.Tasks.Unity.UnityGameObject;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 挂在line renderer 物体上，做圆形指示
/// </summary>
public class Tool : MonoBehaviour
{
    private static Tool ins;
    public static Tool Ins
    {
        get
        {
            if (ins == null)
            {
                ins = new Tool();
            }
            return ins;
        }
    }

    private LineRenderer line;
    private int n = 360;
    private int r = 10;

    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = n+1;
    }

    public void DrawLine(Vector3 centerPoint , int r)
    {
        transform.position = centerPoint;
        for (int i = 0; i <= n; i++)
        {
            this.transform.Rotate(0, 1, 0);
            line.SetPosition(i, this.transform.forward * r);
        }
    }

    public void setActive(bool active)
    {
        gameObject.SetActive(active);
    }
}
