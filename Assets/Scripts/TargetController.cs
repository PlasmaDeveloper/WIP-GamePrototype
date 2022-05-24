using UnityEngine;

public class TargetController : MonoBehaviour
{
    private float health = 50f;

    public void TakeDamage(float damageGet)
    {
        health -= damageGet;

        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }

}
