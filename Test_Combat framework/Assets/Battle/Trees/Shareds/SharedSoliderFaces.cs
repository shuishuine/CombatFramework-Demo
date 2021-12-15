using System.Collections.Generic;
using BehaviorDesigner.Runtime;
using UnityEngine;

namespace Battle
{
    public class SharedSoliderFaces : SharedVariable<List<SoliderFace>>
    {
        public static implicit operator SharedSoliderFaces(List<SoliderFace> value)
        {
            return new SharedSoliderFaces {mValue = value};
        }
    }
}