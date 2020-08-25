using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Собственно сам пул, который выдает свободные объекты по требованию и создает новые при нехватке.
/// </summary>

[AddComponentMenu("Pool/ObjectPooling")]
public class OPool
{

    #region Data
    private List<PoolObject> objects;       // все объекты, содержащиеся в пуле
    private Transform objectsParent;        // родитель в иерархии на сцене
    #endregion

    #region Interface
    public void Initialize(int count, PoolObject sample, Transform objects_parent)
    {
        objects = new List<PoolObject>();
        objectsParent = objects_parent;
        for (int i = 0; i < count; i++)
        {
            AddObject(sample, objects_parent);
        }
    }

    /* Получение объекта. Проходимся по листу, если какой-то из объектов в пуле 
    выключен(т.е.свободен) — возвращаем его, иначе добавляем новый. */

    public PoolObject GetObject()
    {
        for (int i = 0; i < objects.Count; i++)
        {
            if (objects[i].gameObject.activeInHierarchy == false)
            {
                return objects[i];
            }
        }
        AddObject(objects[0], objectsParent);
        return objects[objects.Count - 1];
    }
    #endregion

    #region Methods

    /* Добавление происходит с помощью метода AddObject, принимающего образец, 
       который нужно добавить и родителя в иерархии на сцене. */

    private void AddObject(PoolObject sample, Transform objects_parent)
    {
        GameObject temp;
        temp = GameObject.Instantiate(sample.gameObject);
        temp.name = sample.name;
        temp.transform.SetParent(objects_parent);
        objects.Add(temp.GetComponent<PoolObject>());
        //if (temp.GetComponent<Animator>())
        //    temp.GetComponent<Animator>().StartPlayback();
        temp.SetActive(false);
        Debug.Log("Create object " + sample.gameObject.name);
    }
    #endregion
}