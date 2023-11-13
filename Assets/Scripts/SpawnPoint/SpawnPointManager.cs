using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager Instance;

    public GameObject[] spawnPoints;
    private Dictionary<GameObject, bool> dictFreeSpawnPoints = new Dictionary<GameObject, bool>();

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            dictFreeSpawnPoints.Add(spawnPoints[i], true);
        }
    }

    public Vector3 GetSpawnPoint()
    {
        var freeSpawnPoints = dictFreeSpawnPoints.Where(x => x.Value == true).ToList();

        if (freeSpawnPoints.Count == 0)
        {
            FreeAllSpawnPoint();
            freeSpawnPoints = dictFreeSpawnPoints.Where(x => x.Value == true).ToList();
        }

        int randomIndex = Random.Range(0, freeSpawnPoints.Count - 1);

        GameObject spawnPoint = freeSpawnPoints[randomIndex].Key;

        dictFreeSpawnPoints[spawnPoint] = false;

        return spawnPoint.transform.position;
    }

    public void FreeAllSpawnPoint()
    {
        foreach (var spawnPoint in dictFreeSpawnPoints.Keys.ToList())
        {
            dictFreeSpawnPoints[spawnPoint] = true;
        }
    }
}
