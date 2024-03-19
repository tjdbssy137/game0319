using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{
    public Sprite[] playerFace;
    private int attackCount = 0;

    //bullet
    public GameObject bulletPrefab;
    private float bulletCount = 3;
    public float moveSpeed = 30f;
    private Rigidbody rb;
    private Vector3 position = Vector3.zero;

    private float timeReset = 0;
    private float bulletTime = 3;

    //HP
    private float HP = 100;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInChildren<SpriteRenderer>().sprite = playerFace[attackCount];
        rb = GetComponent<Rigidbody>();
        Fire();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        timeReset += Time.deltaTime;
        if(bulletTime < timeReset)
        {
            Fire();
            timeReset = 0;
        }
        
   }

    void Moving()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        
        position.Set(x, 0, z);
        position = position.normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + position);
    }

    void Fire()
    {
        
        for (int i = 0; i < bulletCount; i++)
        {
            Vector3 bulletDir = Vector3.forward;
            bulletDir.z -= 30f;
            bulletDir.z += 30f * i;
            Quaternion quaternion = Quaternion.Euler(0, bulletDir.z, 0);
            Instantiate(bulletPrefab, transform.position, quaternion);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("enemyBullet"))
        {
            HP -= 34f;
            this.GetComponentInChildren<SpriteRenderer>().sprite = playerFace[++attackCount];
            if(HP < 0)
            {
                Debug.Log("Game Over");
                //UI
            }
        }
        else if (other.gameObject.CompareTag("Item1"))
        {
            Destroy(other.gameObject);
            ++bulletCount;
        }
        else if (other.gameObject.CompareTag("Item2"))
        {
            Destroy(other.gameObject);
            bulletTime -= 0.2f;
        }
    }
}