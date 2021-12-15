using UnityEngine;
using BehaviorDesigner.Runtime;
using BehaviorDesigner.Runtime.Tasks;
using Battle;
using Unity.VisualScripting;

[TaskCategory("Battle")]
public class Create_Bullet : Action
{
	public SharedFloat Angle = 0;
	public string bulletId;
    public Space space;
	public Vector3 offestCenter;

	private Vector3 pos;
	private Vector3 eulerAng;

	public override void OnStart()
	{
		pos = transform.position + offestCenter;
		eulerAng = transform.eulerAngles;
	}

	public override TaskStatus OnUpdate()
	{
        //´´½¨×Óµ¯
        CreateBullet(Quaternion.Euler(eulerAng) * Vector3.forward);
        if (Angle.Value > 0 && Angle.Value <= 360)
        {
            for (int i = 1; i <= Angle.Value / 10; i++)
            {
                CreateBullet(Quaternion.Euler(eulerAng + new Vector3(0, 10 * i, 0)) * Vector3.forward);
                CreateBullet(Quaternion.Euler(eulerAng + new Vector3(0, -10 * i, 0)) * Vector3.forward);
            }
        }
		return TaskStatus.Success;
	}

    private void CreateBullet(Vector3 dir)
    {
        Vector3 bornPos;
        Quaternion bornDir;
        if (space == Space.Self)
        {
            bornPos = transform.TransformPoint(pos);
            dir = transform.TransformDirection(dir);
        }
        else
        {
            bornPos = pos;
        }
        bornDir = Quaternion.LookRotation(dir);

        var bulletObj = GameObject.Instantiate(ResourcesMgr.instance.Load<GameObject>("Bullets/" + bulletId), bornPos, bornDir);
        var tree = bulletObj.AddComponent<BulletTree>();
        var treeAsset = ResourcesMgr.instance.Load<ExternalBehavior>("Bullet_Trees/" + bulletId);

        SkillTree skillTree = null;
        switch (Owner)
        {
            case SkillTree: skillTree = Owner as SkillTree; break;
            case BuffTree: skillTree = (Owner as BuffTree).skillTree; break;
        }
        tree.Init(skillTree);
        if (treeAsset != null)
        {
            tree.ExternalBehavior = GameObject.Instantiate<ExternalBehavior>(treeAsset);
        }
    }
}