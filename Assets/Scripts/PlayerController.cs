using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float movementSpeed;
    public float startingRollSpeed;
    public float rollDropMultiplier;
    public float rollSpeedMinimum;
    public int hitpoints = 100;

    private float rollSpeed;

    public Rigidbody2D rb;
    public Camera cam;
    public GameObject poofPrefab;

    Vector2 moveDir;
    Vector2 mousePos;
    Vector2 rollDir;
    Vector2 lastRollDir;



    private enum State
    {
        Normal,
        Rolling
    }
    private State state;

    private void Start()
    {
        state = State.Normal;
    }
    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case State.Normal:
                moveDir.x = Input.GetAxisRaw("Horizontal");
                moveDir.y = Input.GetAxisRaw("Vertical");
                moveDir.Normalize();


                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    rollDir = moveDir;
                    rollSpeed = startingRollSpeed;
                    state = State.Rolling;
                }
                break;
            case State.Rolling:
                float rollSpeedDropMultiplier = rollDropMultiplier;
                rollSpeed -= rollSpeed * rollSpeedDropMultiplier * Time.deltaTime;


                if (rollSpeed < rollSpeedMinimum)
                {
                    state = State.Normal;
                }
                break;
        }
        mousePos = cam.ScreenToWorldPoint(Input.mousePosition);

    }

    private void FixedUpdate()
    {
        switch (state)
        {
            case State.Normal:
                {
                    rb.velocity = moveDir * movementSpeed;
                    break;
                }
            case State.Rolling:
                {
                    rb.velocity = rollDir * rollSpeed;
                    break;
                }
        }



        Vector2 lookDir = mousePos - rb.position;
        float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg - 90f;
        rb.rotation = angle;


    }

    public void TakeDamage(int damage, Collider2D collider)
    {
        if (state != State.Rolling) hitpoints -= damage;
        if (hitpoints <= 0)
        {
            Instantiate(poofPrefab, transform.position, Quaternion.identity);

            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                enemy.GetComponent<EnemyController>().TakeDmg(1000000);
            }
            GameObject.Find("Main Camera").GetComponent<CameraController>().playerIsNotDead = false;
            GameObject.Find("GameOverCanvas").GetComponent<Canvas>().enabled = true;
            Destroy(gameObject);
        }
        if (collider.gameObject.layer != 8)
        {
            rollDir = collider.transform.up;
            state = State.Rolling;
            rollSpeed = startingRollSpeed;
        }
    }
}
