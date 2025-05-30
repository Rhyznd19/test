using Mirror;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Health : NetworkBehaviour
{
    [Header("Health Settings")]
    [SyncVar(hook = nameof(OnHealthChanged))]
    public int currentHealth = 100;

    public int maxHealth = 100;

    public override void OnStartClient()
    {
        base.OnStartClient();
        OnHealthChanged(currentHealth, currentHealth); // Force UI update
    }

    private void OnHealthChanged(int oldHealth, int newHealth)
    {
        Debug.Log($"[Client] Health changed: {oldHealth} ? {newHealth}");
        // TODO: Update UI here (like health bar)
    }

    [Server]
    public void TakeDamage(int damage)
    {
        if (currentHealth <= 0) return;

        currentHealth -= damage;
        Debug.Log($"[Server] Player took {damage} damage. Health now: {currentHealth}");

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    [Server]
    private void Die()
    {
        Debug.Log("[Server] Player died.");
        // Example: Disable player or respawn
        // NetworkServer.Destroy(gameObject);
    }
}
