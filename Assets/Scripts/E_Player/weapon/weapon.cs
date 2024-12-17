using UnityEngine;
using System.Collections;

public class weapon : MonoBehaviour
{

    [SerializeField] Transform bullet;
    [SerializeField] GameObject bulletPref;
    [SerializeField] GameObject Spawn;
    public int BulletForce = 5000;
    private int Magaz = 30;
    public AudioClip Fire;
    public AudioClip Reload;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetMouseButtonDown(0) && Magaz > 0)
        {
            Instantiate(bulletPref, Spawn.transform.position, Spawn.transform.rotation);
            Magaz = Magaz - 1;
            
        }
        if (Input.GetKeyDown(KeyCode.R))
            Magaz = 30;
        
        Debug.Log(Magaz);
    }
}
