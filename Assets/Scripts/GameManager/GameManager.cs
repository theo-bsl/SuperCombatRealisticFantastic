using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    public int nbPlayer = 2;
    public GameObject PlayerPrefab;
    private List<GameObject> playerList = new List<GameObject>(4);
    private List<Color> playerColors = new List<Color>()
    { Color.blue, Color.red, Color.green, Color.yellow };

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        SpawnPlayer();
    }

    private void Update()
    {
        if (playerList.Count == 1)
        {
            GameOver();
        }
    }

    private void SpawnPlayer()
    {
        for (int i = 0; i < nbPlayer; i++)
        {
            GameObject player = Instantiate(PlayerPrefab);
            playerList.Add(player);
            player.name = "Player" + i;

            player.transform.position = SpawnPointManager.Instance.GetSpawnPoint();

            SetPlayerColor(player, playerColors[i]);

            player.GetComponent<PlayerInput>().SwitchCurrentActionMap("Player " + (i + 1));
        }

        SpawnPointManager.Instance.FreeAllSpawnPoint();
    }

    private void SetPlayerColor(GameObject player, Color color)
    {
        foreach (var renderer in player.GetComponentsInChildren<Renderer>())
        {
            renderer.material.color = color;
        }
    }

    public List<GameObject> PlayerList { get {  return playerList; } }

    public void RemovePlayer(GameObject player)
    { playerList.Remove(player); }

    private void GameOver()
    {

    }
}
