using System;
using System.Collections.Generic;
using UnityEngine;

namespace JYG._Scripts
{
    [CreateAssetMenu(fileName = "ManagerList", menuName = "Scriptable Objects/ManagerList")]
    public class ManagerListData : ScriptableObject
    {
        public ManagerData Managers = new ManagerData();
    }

    [Serializable]
    public class ManagerData
    {
        public List<TestManagerSO> Managers = new List<TestManagerSO>();
    }
}
