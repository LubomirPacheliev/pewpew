using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject poofPrefab;

    public int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(poofPrefab, transform.position, transform.rotation);

        if(collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<EnemyController>().TakeDmg(damage);
        }

        Destroy(gameObject);       
    }
}
