using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveRoom : MonoBehaviour
{
    public bool IsRemovable = true;

    void OnEnable()
    {
        IsRemovable = true;
    }

    public void DeleteRoom()
    {
        if (IsRemovable)
        {
            ObjectPoolManager.ReturnObjectToPool(gameObject);
        }
    }
}
