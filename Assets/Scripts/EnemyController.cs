using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;


public class EnemyController : MonoBehaviour
{
    public Rigidbody2D rb;
    public GameObject poofPrefab;
    private Transform player;
    
    public float stunTime;
    public float speed;
    public int hitPoints;
    public int bodyDamage;
    public float stun;
    public float stopDistance;
    private NavMeshAgent agent;
    private void Awake()
    {
        player = GameObject.Find("Player").GetComponent<Transform>();
        stun = 0f;
        agent = GetComponent<NavMeshAgent>();
        agent.updateRotation = false;
        agent.updateUpAxis = false;
        agent.speed = speed;
        agent.stoppingDistance = stopDistance;
    }

    private void Update()
    {
        //transform.position = Vector2.MoveTowards(transform.position, player.position, speed * Time.deltaTime)

        if (stun < Time.time)
        {
            Vector2 lookDir = player.position - transform.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = angle;
            
        }
    }

    private void FixedUpdate()
    {
        if (stun < Time.time)
        {
            rb.velocity = new Vector2(0, 0);
            agent.enabled = true;
            agent.SetDestination(player.position);
        }
    }

    public void TakeDmg(int damageTaken)
    {
        hitPoints -= damageTaken;
        if(hitPoints <= 0)
        {
            Instantiate(poofPrefab, transform.position, transform.rotation);
            Destroy(gameObject);
        }
        rb.velocity = new Vector2(0, 0);
        agent.enabled = false;
        rb.AddForce(-transform.up * 10f, ForceMode2D.Impulse);
        stun = Time.time + stunTime;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 3)
        {
            collision.gameObject.GetComponent<PlayerController>().TakeDamage(bodyDamage, collision.otherCollider);
            stunTime *= 2;
            TakeDmg(bodyDamage / 2);
            stunTime /= 2;
        }
        if(collision.gameObject.layer == 7)
        {
            if(collision.gameObject.GetComponent<EnemyController>().stun > Time.time)
            {
                stunTime *= 2;
                TakeDmg(0);
                stunTime /= 2;
            }
        }
    }
}
