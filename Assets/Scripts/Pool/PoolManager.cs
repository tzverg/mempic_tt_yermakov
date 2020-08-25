using UnityEngine;

/// <summary>
/// управляет пулами различных объектов. Класс статический для упрощения доступа к объектам, 
/// т.е. не нужно создавать синглтоны, инстансы и прочее.
/// </summary>

public static class PoolManager
{
    private static PoolPart[] pools;
    private static GameObject objectsParent;

    [System.Serializable]
    public struct PoolPart
    {
        public string name;             //имя префаба
        public PoolObject prefab;       //сам префаб, как образец
        public int count;               //количество объектов при инициализации пула
        public OPool ferula;            //сам пул
    }

    public static void Initialize(PoolPart[] newPools)
    {
        pools = newPools;
        objectsParent = new GameObject();
        objectsParent.name = "Pool";
        for (int i = 0; i < pools.Length; i++)
        {
            if (pools[i].prefab != null)
            {
                pools[i].ferula = new OPool();
                pools[i].ferula.Initialize(pools[i].count, pools[i].prefab, objectsParent.transform);
            }
        }
    }

    /* Метод проверяет все существующие пулы, и если находит правильный — дергает его метод GetObject() 
     * у класса OPool */

    public static GameObject GetObject(string name, Vector3 position, Vector3 localScale)
    {
        GameObject result = null;
        if (pools != null)
        {
            for (int i = 0; i < pools.Length; i++)
            {
                if (string.Compare(pools[i].name, name) == 0)
                {
                    result = pools[i].ferula.GetObject().gameObject;
                    result.transform.position = position;
                    result.transform.localScale = localScale;
                    result.SetActive(true);
                    return result;
                }
            }
        }
        return result;
    }
}