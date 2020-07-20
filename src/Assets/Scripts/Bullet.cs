using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 70f;
    public float effectDuration = 2f;

    public GameObject impactEffect;
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
        
    }

    void HitTarget()
    {
        GameObject effect = (GameObject)Instantiate(impactEffect, transform.position, transform.rotation);
        Destroy(effect, effectDuration);
        Destroy(gameObject);
        
    }


}
