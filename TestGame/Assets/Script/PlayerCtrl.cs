using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerCtrl : MonoBehaviour
{

    public Sprite[] playerFace;
    public Sprite[] startPlayerFace;
    public GameObject[] bulletPrefab;
    private int attackCount = 0;
    private int StartCharacter = ChooseCharacter.Instance.CharInfo;

    //bullet
    private float bulletCount = 3;
    public float moveSpeed = 3f;
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
    bool isRestart = false;
    float timeReset2 = 0;

    // Start is called before the first frame update
    void Start()
    {
        playerFace[attackCount] = startPlayerFace[StartCharacter];
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

        if(isRestart)
        {
            Restart();
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
            Instantiate(bulletPrefab[StartCharacter], transform.position, quaternion);
        }
    }
    void Restart()
    {
        timeReset2 += Time.deltaTime;
        if (1 < timeReset2)
        {
            textUI.text = "Restart.. " + time--.ToString() + "...";
            timeReset2 = 0;
        }
        if (time < 0)
        {
            SceneManager.LoadScene(0);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.CompareTag("enemyBullet"))
        {
            StartCoroutine(DamagePlayer());
            Destroy(other.gameObject);
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
            bulletTime -= 0.1f;
        }
        else if (other.gameObject.CompareTag("Item3"))
        {
            Destroy(other.gameObject);
            if(HP + 10f < 100)
            {
                HP += 10;
            }
            else
            {
                HP = 100;
            }

            playerHP.fillAmount = HP / 100;

            if (90 <= HP)
            {
                this.GetComponentInChildren<SpriteRenderer>().sprite = playerFace[1];
            }
            else if(50 <= HP && HP < 80)
            {
                this.GetComponentInChildren<SpriteRenderer>().sprite = playerFace[1];
            }
            else if(HP < 50)
            {
                this.GetComponentInChildren<SpriteRenderer>().sprite = playerFace[2];
            }
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
                isRestart = true;
                break;
            }

            playerHP.fillAmount = HP / 100;
            yield return new WaitForSeconds(0.01f);
        }
    }

}