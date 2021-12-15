using UnityEngine;

namespace Battle
{
    public class SoliderData
    {
        public SoliderConfig config;

        public SoliderData(SoliderConfig config)
        {
            this.config = config;   
        }

        public override string ToString()
        {
            return $"最大生命值 :{config.bloodMax}; 生命值:{config.blood}";
        }
    }
}