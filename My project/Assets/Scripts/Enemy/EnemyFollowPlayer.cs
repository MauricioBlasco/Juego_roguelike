using UnityEngine;

public class EnemyFollowPlayer : MonoBehaviour
{
    public float speed = 2f;           // Velocidad de movimiento
    private Transform player;          // Referencia al Player

    void Start()
    {
        // Buscar automáticamente al objeto con tag "Player"
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("No se encontró ningún objeto con el tag 'Player'.");
        }
    }

    void Update()
    {
        if (player == null) return;

        // Calcular dirección hacia el jugador
        Vector2 direction = (player.position - transform.position).normalized;

        // Mover hacia el jugador
        transform.position += (Vector3)(direction * speed * Time.deltaTime);
    }
}
