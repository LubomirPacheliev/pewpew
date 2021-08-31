using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyShooting : MonoBehaviour
{

    public Transform mainFirePoint;
    public GameObject bulletPrefab;

    public float bulletForce = 20f;
    public int bulletDamage = 10;
    public float reload = 0.1f;
    public float timeBtwBullets = 0.1f;
    public int numberOfBullets = 2;


    private float time = 0;

    // Update is called once per frame
    void Update()
    {
            if (time < Time.time)
            {
                //Shoot();
                StartCoroutine(Shoot());
                time = Time.time + reload;
            }    
    }

    void oneShot(Transform firepoint)
    {
        GameObject bullet = Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        bullet.GetComponent<EnemyBullet>().damage = bulletDamage;
        rb.AddForce(firepoint.up * bulletForce, ForceMode2D.Impulse);
    }

    
    IEnumerator Shoot()
    {
        for (int i = 0; i < numberOfBullets; i++)
        {
            oneShot(mainFirePoint);
            yield return new WaitForSeconds(timeBtwBullets);
        }
    }
}
