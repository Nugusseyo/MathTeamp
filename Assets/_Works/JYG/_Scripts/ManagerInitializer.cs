using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace JYG._Scripts
{
    public class ManagerInitializer : MonoBehaviour
    {
        [field:SerializeField] public PlayerInputSO PlayerInputSO { get; private set; }
        [SerializeField] private ManagerListData managerListData;
        
        public GameManager GameManager { get; private set; }
        private void Awake()
        {
            //List<IManager> managers = managerListData.Managers.Managers.ToList(x => x.);
            GameManager = new GameManager(, PlayerInputSO);
        }
    }
}
