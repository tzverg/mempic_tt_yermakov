using UnityEngine;

/// <summary>
/// Компонент Pool Object должен находиться на любом объекте, используемом в пуле. 
/// Его основное предназначение — вернуть объект обратно в пул.
/// </summary>

[AddComponentMenu("Pool/PoolObject")]
public class PoolObject : MonoBehaviour
{
    #region Interface
    public void ReturnToPool()
    {
        gameObject.SetActive(false);
    }
    #endregion
}