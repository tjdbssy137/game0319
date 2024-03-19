using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
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
    public Image playerHP;

    //GUI
    public GameObject gameover;
    public Text textUI;
    int time = 3;

    // Start is called before the first frame update
    void Start()
    {
        this.GetComponentInChildren<SpriteRenderer>().sprite = playerFace[attackCount];
        rb = GetComponent<Rigidbody>();
        Fire();
        gameover.SetActive(false);
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

    void showString()
    {
        while (1 < time)
        {
            StartCoroutine(Timer());
        }
        SceneManager.LoadScene(0);
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("enemyBullet"))
        {
            StartCoroutine(DamagePlayer());
            this.GetComponentInChildren<SpriteRenderer>().sprite = playerFace[++attackCount]; 
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
        else if (other.gameObject.CompareTag("Item3"))
        {
            Destroy(other.gameObject);
            HP += 34f;
        }
    }

    IEnumerator DamagePlayer()
    {
        float temp = HP;
        while (temp - 34f <= HP)
        {
            HP--;

            if (HP <= 0)
            {
                gameover.SetActive(true);
                showString();
                Time.timeScale = 0;
            }

            playerHP.fillAmount = HP / 100;
            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator Timer()
    { 
        yield return new WaitForSeconds(1f);
        time--;
    }
}