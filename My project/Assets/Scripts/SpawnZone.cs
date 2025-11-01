using UnityEngine;

public class SpawnZone : MonoBehaviour
{
    public GameObject prefab;
    public float spawnInterval = 5f;
    public Vector2 areaSize = new Vector2(10f, 5f);
    public BoxCollider2D playerCollider;  // Referencia al collider del jugador
    public int maxAttempts = 20;          // Cuántas veces intentamos buscar una posición válida

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObject), 0f, spawnInterval);
    }

    void SpawnObject()
    {
        for (int i = 0; i < maxAttempts; i++)
        {
            Vector2 randomPos = new Vector2(
                Random.Range(-areaSize.x / 2, areaSize.x / 2),
                Random.Range(-areaSize.y / 2, areaSize.y / 2)
            );

            Vector2 spawnPos = (Vector2)transform.position + randomPos;

            // Si la posición está fuera del área del jugador, instanciamos
            if (!IsInsidePlayer(spawnPos))
            {
                Instantiate(prefab, spawnPos, Quaternion.identity);
                return;
            }
        }

        Debug.LogWarning("No se encontró posición válida para spawnear (zona del jugador muy grande).");
    }

    bool IsInsidePlayer(Vector2 position)
    {
        if (playerCollider == null)
            return false;

        // Comprobamos si el punto está dentro del collider del jugador
        return playerCollider.OverlapPoint(position);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireCube(transform.position, areaSize);

        if (playerCollider != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(playerCollider.bounds.center, playerCollider.bounds.size);
        }
    }
}
