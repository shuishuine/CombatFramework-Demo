using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Battle;
using System.Collections.Generic;

[TaskCategory("Battle")]
public class CheckTrigger_Single : Conditional
{
    //public SharedSoliderFaces soliderFaces;
    public SharedTransform target;
    public List<int> layers = new List<int>();

    private int layerSelf;
    private bool isTrigger = false;

    public override void OnAwake()
    {
        //soliderFaces.Value.Clear();
        switch (Owner)
        {
            case SkillTree: layerSelf = gameObject.layer; break;
            case BulletTree: layerSelf = (Owner as BulletTree).soliderFace.gameObject.layer; break;
        }
    }

    public override TaskStatus OnUpdate()
	{
        return isTrigger ? TaskStatus.Success : TaskStatus.Failure;
    }

    public override void OnEnd()
    {
        isTrigger = false;
    }

    public override void OnTriggerEnter(Collider other)
    {
        foreach (var item in layers)
        {
            if (other.gameObject.layer.Equals(item) && other.gameObject.layer != layerSelf)
            {
                //soliderFaces.Value.Add(other.transform.GetComponent<SoliderFace>());
                target.Value = other.transform;
                isTrigger = true;
            }
        }
    }
}