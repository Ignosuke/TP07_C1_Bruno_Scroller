using UnityEngine;

// Devuelve al jugador al ultimo punto firme cuando el golpe vino de una layer de peligro (Lethal)
[RequireComponent(typeof(Rigidbody2D))]
// Error: La clase HazardRespawn está en un archivo que se llama distinto (PlayerHazardRespawn.cs).
// Unity lo compila pero tira un warning.
// Renombrá el archivo o la clase para que coincidan.
public class HazardRespawn : MonoBehaviour
{
    [SerializeField] private PlayerSo playerData;
    [SerializeField] private PlayerHealth health;
    [SerializeField] private SafePointTracker safePointTracker;

    private LayerMask respawnLayers;
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        respawnLayers = playerData.GetRespawnLayers();
    }

    private void OnEnable()
    {
        health.OnDamaged += HandleDamaged;
    }

    private void OnDisable()
    {
        health.OnDamaged -= HandleDamaged;
    }

    private void HandleDamaged(DamageInfo info)
    {
        if (!info.IsFrom(respawnLayers)) return;

        if (health.IsDead) return; // Era la ultima vida: no hay respawn

        Respawn();
    }

    private void Respawn()
    {
        rb.linearVelocity = Vector2.zero;
        transform.position = safePointTracker.LastSafePoint;
    }
}