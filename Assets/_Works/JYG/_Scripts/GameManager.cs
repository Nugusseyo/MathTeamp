using System;
using System.Collections.Generic;
using System.Linq;
using NKT.Manager;
using UnityEngine.EventSystems;

namespace JYG._Scripts
{
    public class GameManager
    {
        private PlayerInputSO _inputSO;
        private CostManager _costManager;

        private Dictionary<Type, IManager> _managerDict = new Dictionary<Type, IManager>();

        public GameManager(IEnumerable<IManager> managers, PlayerInputSO input)
        {
            foreach (IManager manager in managers)
            {
                manager.Init(this);
                _managerDict.TryAdd(manager.GetType(), manager);
            }
            _inputSO = input;
            input.OnMouseClick += HandlePlayerClick;
        }
        
        ~GameManager()
        {
            _inputSO.OnMouseClick -= HandlePlayerClick;
        }

        private T GetManager<T>()
        {
            T returnManager = default(T);
            if(_managerDict.TryGetValue(typeof(T), out IManager manager))
                returnManager = (T)manager;

            if (returnManager == null)
                returnManager = (T)_managerDict.Values.FirstOrDefault(target => target is T);
            
            return returnManager;
        }

        private void HandlePlayerClick()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;
            
            _costManager.AddMoney(5);
        }
    }
}
