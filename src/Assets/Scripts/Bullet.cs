using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70f;
    public float effectDuration = 2f;

    public GameObject impactEffect;
    public float impactRadius = 0f;

    Transform target;    


    public void Seek (Transform _target)
    {
        target = _target;
    }

    // Update is called once per frame
    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 dir = target.position - transform.position;
        float distancePerFrame = speed * Time.deltaTime;

        if(dir.magnitude <= distancePerFrame)
        {
            HitTarget();            
            return;
        }

        transform.Translate(dir.normalized * distancePerFrame, Space.World);
        transform.LookAt(target);
        
    }

    void HitTarget()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, effectDuration);

        if(impactRadius > 0f)
        {
            Explode();
        }
        else
        {
            Damage(target);
        }

        Destroy(transform.gameObject);
        
    }

    void Explode()
    {
        Collider[] affected = Physics.OverlapSphere(transform.position, impactRadius);
        foreach(Collider obj in affected)
        {
            if (obj.tag == "enemy")
                Damage(obj.transform);
        }
    }

    void Damage(Transform enemy)
    {
        Destroy(enemy.gameObject);
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, impactRadius);
    }


}
