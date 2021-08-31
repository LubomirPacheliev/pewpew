using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class Shooting : MonoBehaviour
{

    public Transform mainFirePoint;
    public Transform secondaryFirePoint1;
    public Transform secondaryFirePoint2;
    public GameObject bulletPrefab;

    private float bulletForce = 20f;
    public int bulletDamage = 10;
    public float reload = 0.1f;
    public float timeBtwBullets = 0.1f;
    public int numberOfBullets = 2;


    private float time = 0;
    private bool isShotgun = false;

    private void Start()
    {
        Physics2D.IgnoreLayerCollision(6, 6); // friendly bullets with themselves  
        Physics2D.IgnoreLayerCollision(3, 6); // player with friendly bullets
        Physics2D.IgnoreLayerCollision(7, 8); // enemy bullets with themselves  
        Physics2D.IgnoreLayerCollision(8, 8); // player with enemy bullets
    }
    // Update is called once per frame
    void Update()
    {

        if (Input.GetButton("Fire1"))
        {
            if (time < Time.time)
            {
                //Shoot();
                StartCoroutine(Shoot());
                Debug.Log(Time.time);
                time = Time.time + reload;
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) // Machine gun
        {
            isShotgun = false;
            time += 0.5f;
            bulletForce = 20f;
            bulletDamage = 10;
            reload = 0.1f;
            timeBtwBullets = 0;
            numberOfBullets = 1;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) // Burst gun
        {
            isShotgun = false;
            time += 0.5f;
            bulletForce = 20f;
            bulletDamage = 20;
            reload = 1f;
            timeBtwBullets = 0.05f;
            numberOfBullets = 5;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) // Shotgun
        {
            isShotgun = true;
            time += 0.5f;
            bulletForce = 20f;
            bulletDamage = 34;
            reload = 0.5f;
            timeBtwBullets = 0f;
            numberOfBullets = 1;
        }

    }

   void oneShot(Transform firepoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<Bullet>().damage = bulletDamage;
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
    }
   

    IEnumerator Shoot()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            oneShot(mainFirePoint);

            if (isShotgun)
            {
                oneShot(secondaryFirePoint1);
                oneShot(secondaryFirePoint2);
            }

            yield return new WaitForSeconds(timeBtwBullets);
        }
    }
}
