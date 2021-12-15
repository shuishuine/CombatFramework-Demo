using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class MessageCenter : Singleton<MessageCenter>
{
    Dictionary<int, Action<object>> dic = new Dictionary<int, Action<object>>();

    public void AddListener(int id, Action<object> act)
    {
        if (dic.ContainsKey(id))
        {
            dic[id] = dic[id] + act;
        }
        else
        {
            dic.Add(id, act);
        }
    }

    public void BrodCast(int id, params object[] obj)
    {
        if (dic[id] != null)
        {
            dic[id](obj);
        }
    }

    public override void Dispose()
    {
        
    }

    public override void Init()
    {
        
    }
}

