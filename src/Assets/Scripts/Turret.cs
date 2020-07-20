using UnityEngine;

public class Turret : MonoBehaviour
{

    [Header("Attributes")]
    public float range = 15f;
    public float turnSpeed = 10f;
    public float fireRate = 1f;

    float fireCountdown = 0f;

    [Header("Unity Configuration")]    
    public Transform partToRotate;
    public string enemyTag = "enemy";
    public GameObject bulletPrefab;
    public Transform firePoint;

    Transform target;    

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("UpdateTarget", 0.0f, 0.5f);
    }

    void UpdateTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(enemyTag);
        float shortestDistance = Mathf.Infinity;
        GameObject closestEnemy = null;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);
            if (shortestDistance > distanceToEnemy)
            {
                shortestDistance = distanceToEnemy;
                closestEnemy = enemy;
            }
        }

        if (shortestDistance <= range)
            target = closestEnemy.transform;
        else
            target = null;

    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        //Target Look on
        Vector3 dir = target.position - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(dir);
        Vector3 rotation = Quaternion.Lerp(partToRotate.rotation, lookRotation, Time.deltaTime * turnSpeed).eulerAngles;
        partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
        
        if(fireCountdown <= 0)
        {
            Shoot();
            fireCountdown = 1f / fireRate;
        }

        fireCountdown -= Time.deltaTime;
        
    }

    void Shoot()
    {
        Debug.Log("Shoot");
        GameObject bulletGO = (GameObject)Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Bullet bullet = bulletGO.GetComponent<Bullet>();
        if(target != null)
            bullet.Seek(target);
    }

    

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, range);
    }
}
