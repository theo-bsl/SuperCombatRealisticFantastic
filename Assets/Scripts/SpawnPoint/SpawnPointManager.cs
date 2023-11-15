using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpawnPointManager : MonoBehaviour
{
    public static SpawnPointManager Instance;

    public GameObject[] spawnPoints;
    private Dictionary<GameObject, bool> dictFreeSpawnPoints = new Dictionary<GameObject, bool>(4);

    private List<int> freeSpawnPointsIndex = new List<int>();

    private List<GameObject> playerList = new List<GameObject>(4);
    private int distanceToReactivate = 5;

    private void Awake()
    {
        Instance = this;

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            dictFreeSpawnPoints.Add(spawnPoints[i], true);
        }

        playerList = GameManager.Instance.PlayerList;
    }

    public Vector3 GetSpawnPoint()
    {
        freeSpawnPointsIndex.Clear();
        SetFreeSpawnPoint();

        for (int i = 0; i < spawnPoints.Length - 1; i++)
        {
            int index = GetIndex();
            freeSpawnPointsIndex.Remove(index);

            if (IsThisPointFree(spawnPoints[index]))
            {
                return spawnPoints[index].transform.position;
            }
        }

        Debug.Log(freeSpawnPointsIndex.Count);
        return spawnPoints[spawnPoints.Length - 1].transform.position;
    }

    private void SetFreeSpawnPoint()
    {
        for (int i = 0; i < spawnPoints.Length - 1; i++)
        {
            freeSpawnPointsIndex.Add(i);
        }
    }
    private int GetIndex()
    {
        int index = Random.Range(0, freeSpawnPointsIndex.Count);
        if (freeSpawnPointsIndex.Count > 0)
        {
            return freeSpawnPointsIndex[index];
        }
        else
            return spawnPoints.Length - 1;
    }

    private bool IsThisPointFree(GameObject spawn)
    {
        for (int j = 0; j < playerList.Count; j++)
        {
            float distance = Vector3.Distance(spawn.transform.position, playerList[j].transform.position);
            if (distance < distanceToReactivate)
            {
                return false;
            }
        }
        return true;
    }

    public void FreeAllSpawnPoint()
    {
        foreach (var spawnPoint in dictFreeSpawnPoints.Keys.ToList())
        {
            dictFreeSpawnPoints[spawnPoint] = true;
        }
    }
}
