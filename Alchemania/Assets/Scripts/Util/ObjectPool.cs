using UnityEngine;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour {

    private Dictionary<string, List<ISpawnableObject>> spawnMap;

    void Awake()
    {
        spawnMap = new Dictionary<string, List<ISpawnableObject>>();
    }

    private void Init()
    {
       // spawnMap.Add("Fireball", new List<null>());
    }

}
