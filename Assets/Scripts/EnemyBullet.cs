using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    public GameObject poofPrefab;

    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(poofPrefab, transform.position, transform.rotation);

        if (collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(damage, collision.otherCollider);
        }

        Destroy(gameObject);
    }
}
