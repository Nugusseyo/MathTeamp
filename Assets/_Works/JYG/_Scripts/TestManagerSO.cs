using JYG._Scripts;
using UnityEngine;

[CreateAssetMenu(fileName = "TestManagerSO", menuName = "Scriptable Objects/TestManagerSO")]
public class TestManagerSO : ScriptableObject, IManager
{
    public IManager Manager => this;
    public void Init(GameManager gameManager)
    {
        
    }
}
