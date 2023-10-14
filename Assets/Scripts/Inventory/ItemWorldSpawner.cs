using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorldSpawner : MonoBehaviour
{
    public Item _item;

    // make functions to randomize items and automize spawns in the map

    private void Start()
    {
        ItemWorld.SpawnItemWorld(transform.position, _item);
        Destroy(this.gameObject);
    }
}
