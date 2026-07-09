using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JYG._Scripts
{
    public class ManagerInitializer : MonoBehaviour
    {
        private Dictionary<Type, ManagerBase> _managers = new Dictionary<Type, ManagerBase>();
        public List<ManagerBase> initManagers = new List<ManagerBase>();
        private void Awake()
        {
            _managers = initManagers.ToDictionary(x => x.GetType(), x => x);
            foreach (ManagerBase manager in initManagers)
            {
                manager.Initialize(this);
            }
        }

        public T GetManager<T>() where T : ManagerBase
        {
            if (_managers.TryGetValue(typeof(T), out ManagerBase manager))
            {
                return (T)manager;
            }

            Debug.LogWarning("객체가 원하는 SO가 존재하지 않습니다.");
            return null;
        }
    }
}
