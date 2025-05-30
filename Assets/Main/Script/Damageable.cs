using UnityEngine;
using Mirror;

public class Damageable : NetworkBehaviour
{
    [SerializeField] private int damage = 10;

    private Health currentTarget;

    private void Update()
    {
        //if (!isLocalPlayer) return;

        if (Input.GetKeyDown(KeyCode.G) && currentTarget != null)
        {
            Debug.Log("[Client] G key pressed, target in range.");
            CmdDealDamage(currentTarget.netIdentity, damage);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (!isLocalPlayer) return;

        if (other.CompareTag("Player") && other.gameObject != gameObject)
        {
            Health targetHealth = other.GetComponent<Health>();
            if (targetHealth != null)
            {
                Debug.Log("[Client] Target in range.");
                currentTarget = targetHealth;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        //if (!isLocalPlayer) return;

        if (other.CompareTag("Player") && other.gameObject != gameObject)
        {
            Health targetHealth = other.GetComponent<Health>();
            if (targetHealth != null && currentTarget == targetHealth)
            {
                Debug.Log("[Client] Target out of range.");
                currentTarget = null;
            }
        }
    }

    [Command]
    private void CmdDealDamage(NetworkIdentity targetId, int damage)
    {
        Health target = targetId.GetComponent<Health>();
        if (target != null)
        {
            Debug.Log($"[Server] Dealing {damage} to {target.gameObject.name}");
            target.TakeDamage(damage);
        }
    }
}
