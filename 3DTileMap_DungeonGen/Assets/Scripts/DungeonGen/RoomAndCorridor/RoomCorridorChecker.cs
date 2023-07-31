using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCorridorChecker : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Room"))
            collision.gameObject.GetComponent<RemoveRoom>().IsRemovable = false;
    }
}
