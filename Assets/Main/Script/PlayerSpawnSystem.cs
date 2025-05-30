using Mirror;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerSpawnSystem : NetworkBehaviour
{
    [SerializeField] GameObject playerPrefab = null;    

    private static List<Transform> spawnPoints = new List<Transform>();

    private int nextIndex = 0;

    public static void AddSpawnPoint(Transform transform)
    {
        spawnPoints.Add(transform);

        spawnPoints = spawnPoints.OrderBy(x => x.GetSiblingIndex()).ToList();
    }

    public static void RemoveSpawnPoint(Transform transform)
    {
        spawnPoints.Remove(transform);
    }

    public override void OnStartServer()
    {
        NetworkManagerLobby.OnServerReadyToBegin += SpawnPlayer;
    }

    [ServerCallback]
    private void OnDestroy()
    {
         NetworkManagerLobby.OnServerReadyToBegin -= SpawnPlayer;
    }

    [Server]
    private void SpawnPlayer(NetworkConnection conn)
    {
        Transform spawnPoint = spawnPoints.ElementAtOrDefault(nextIndex);

        if (spawnPoint == null)
        {
            Debug.Log($"Missing spawn point for player {nextIndex}");
            return;
        }

        GameObject playerInstance = Instantiate(playerPrefab, spawnPoints[nextIndex].position, spawnPoints[nextIndex].rotation);
        NetworkServer.Spawn(playerInstance, conn.identity.gameObject);

        nextIndex++;
    
    }
}
