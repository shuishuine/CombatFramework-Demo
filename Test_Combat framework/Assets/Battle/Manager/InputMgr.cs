using Battle;
using UnityEngine;
using UnityEngine.AI;

public class InputMgr : MonoBehaviour
{
    public GameObject skillUIItem;
    public Transform skillUIParent;
    public SoliderFace soliderFace;
    private GameObject targetObj;




    private void Start()
    {
        if (soliderFace == null)
        {
            soliderFace = GameObject.Find("player").GetComponent<SoliderFace>() ;
        }

        //����ui��ʼ��
        if (skillUIItem && skillUIParent)
        {
            UI_SkillMgr.instance.Init(skillUIItem, skillUIParent, soliderFace.SoliderData.config.heroId);
        }
    }

    void Update()
    {
        if (soliderFace.MouseInput)
        {
            UserMouseInput();
        }
        if (soliderFace.SkillInput)
        {
            UserSkillInput();
        }
    }




    /// <summary>
    /// �û������ⲿ�������
    /// </summary>
    private void UserMouseInput()
    {
        if (Input.GetMouseButtonDown(1))
        {
            if (CameraToRay_FromMouse(out var hit, Mathf.Infinity, 1 << 3))
            {
                targetObj = null; //���ѡ��
                soliderFace.Move(hit.point);
            }
        }

        if (Input.GetMouseButtonDown(0))
        {
            if (CameraToRay_FromMouse(out var hit, Mathf.Infinity, 1 << 6))
            {
                targetObj = hit.transform.gameObject;
            }
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            soliderFace.ReleaseSkill(-1);
            soliderFace.SendEventSkill(-1, "again");
        }
    }

    /// <summary>
    /// �û����е��ⲿ��������
    /// </summary>
    private void UserSkillInput()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            LookAtMouse();
            soliderFace.ReleaseSkill(1);
            soliderFace.SendEventSkill(1, "start");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            LookAtMouse();
            soliderFace.ReleaseSkill(2);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            LookAtMouse();
            soliderFace.ReleaseSkill(3);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            if (targetObj)
            {
                soliderFace.ReleaseSkill(4, targetObj);
            }
        }
    }

    /// <summary>
    /// �������
    /// </summary>
    private void LookAtMouse()
    {
        RaycastHit hit;
        if (CameraToRay_FromMouse(out hit, Mathf.Infinity, 1 << 3))
        {
            soliderFace.transform.LookAt(hit.point);
        }
    }

    /// <summary>
    /// ���������һ������������
    /// </summary>
    /// <param name="hit"></param>
    /// <param name="dis"></param>
    /// <param name="layer"></param>
    /// <returns></returns>
    private bool CameraToRay_FromMouse(out RaycastHit hit, float dis, int layer)
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, dis, layer))
        {
            return true;
        }
        return false;
    }
}
