using UnityEngine;

/// <summary>
/// Обертка для управления статическим классом PoolManager
/// </summary>

[AddComponentMenu("Pool/PoolSetup")]
public class PoolSetup : MonoBehaviour
{
    #region Unity scene settings
    [SerializeField] private PoolManager.PoolPart[] pools;
    #endregion

    #region Methods

    //присваиваем имена заранее, до инициализации

    void OnValidate()
    {
        for (int i = 0; i < pools.Length; i++)
        {
            pools[i].name = pools[i].prefab.name;
        }
    }

    void Awake()
    {
        Initialize();
    }

    void Initialize()
    {
        PoolManager.Initialize(pools);
    }
    #endregion
}