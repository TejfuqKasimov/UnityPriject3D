using UnityEngine;
using System.Collections;

public class weaponPistol : MonoBehaviour
{
    [SerializeField] GameObject bulletPref;
    [SerializeField] GameObject Spawn;
    [SerializeField] private float spread = 10;
    [SerializeField] private int BulletForce = 6000;
    [SerializeField] private int Magaz = 7;

    public Camera cam;
    

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Magaz > 0)
        {
            Shoot();
            Magaz = Magaz - 1;
            
        }
        if (Input.GetKeyDown(KeyCode.R))
            Magaz = 7;
    }

    private void Shoot()
    {
        // Ray ray = Spawn.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;

        Vector3 targetPoint;
        if (Physics.Raycast(ray, out hit))
            targetPoint = hit.point;
        else
            targetPoint = ray.GetPoint(100);

        Vector3 DirBullet = targetPoint - Spawn.transform.position;

        float RayX = Random.Range(-spread, spread);
        float RayY = Random.Range(-spread, spread);

        Vector3 DirBulletWithSpread = new Vector3(RayX, RayY, 0) + DirBullet;


        GameObject currentBullet = Instantiate(bulletPref, Spawn.transform.position, Spawn.transform.rotation); //Quaternion.Euler(DirBulletWithSpread.normalized)
        // currentBullet.transform.forward = DirBulletWithSpread.normalized;
        currentBullet.GetComponent<Rigidbody>().AddForce(DirBulletWithSpread.normalized * BulletForce, ForceMode.Impulse);
        
    }
}
