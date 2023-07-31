using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetMainRooms : MonoBehaviour
{
    [SerializeField] private DungeonGenData _data;

    public void SortGameObjectsByCollider()
    {
        _data.MainRooms.Clear();

        // Create a list to store the game objects and their corresponding collider sizes
        List<KeyValuePair<GameObject, float>> colliderSizes = new List<KeyValuePair<GameObject, float>>();

        GameObject[] availableRooms = GameObject.FindGameObjectsWithTag("Room");

        // Iterate through each game object and store its collider size in the list
        foreach (GameObject obj in availableRooms)
        {
            BoxCollider collider = obj.GetComponent<BoxCollider>();
            if (collider != null)
            {
                float size = collider.size.magnitude;
                colliderSizes.Add(new KeyValuePair<GameObject, float>(obj, size));
            }
        }

        // Sort the game objects based on collider size from large to small
        colliderSizes.Sort((x, y) => y.Value.CompareTo(x.Value));

        // Extract the sorted game objects into a new array
        GameObject[] sortedGameObjects = new GameObject[colliderSizes.Count];
        for (int i = 0; i < colliderSizes.Count; i++)
        {
            sortedGameObjects[i] = colliderSizes[i].Key;
        }

        // Replace the original _data.Rooms array with the sorted array
        for (int i = 0; i < 5; i++)
        {
            _data.MainRooms.Add(sortedGameObjects[i]);

            //Debug.Log("MainRoomsMade = " + _data.MainRooms.Count);
        }

        if(_data.dungeonType == algoritmType.rogue)
        for (int i = 5; i < sortedGameObjects.Length; i++)
        {
                ObjectPoolManager.ReturnObjectToPool(sortedGameObjects[i]);
        }
        //gameObject.GetComponent<BowyerWatson>().StartAlgorithm();
        _data.Rooms = _data.MainRooms;
        gameObject.GetComponent<Prim>().StartAlgorithm();
        //--<STARTALGORITM>--
    }
}
