using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EmemyMove : MonoBehaviour
{
    private GameObject Player;
    private float speed = 0.6f;

    public GameObject[] dropItem;
    //bullet
    public GameObject enemyBullet;
    private float timeReset = 0;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindWithTag("Player");
        EnemyFire();
    }

    // Update is called once per frame
    void Update()
    {
        Moving();
        timeReset += Time.deltaTime;
        if (3 < timeReset)
        {
            EnemyFire();
            timeReset = 0;
        }
    }

    void Moving()
    {
        this.transform.LookAt(Player.transform);
        this.transform.position += transform.forward * speed * Time.deltaTime;
    }

    void EnemyFire()
    {
        Instantiate(enemyBullet, this.transform.position, this.transform.rotation);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("bullet"))
        {
            //���� Ȯ���� ������ ����
            
            int random = Random.Range(0, 9);
            switch(random)
            { 
                case 1:
                    Instantiate(dropItem[0], transform.position, transform.rotation);//���� �ڸ����� �ȿ����̰�..
                    break;
                case 2:
                    Instantiate(dropItem[1], transform.position, transform.rotation);
                    break;
                case 3:
                    Instantiate(dropItem[2], transform.position, transform.rotation);
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
            --EnemySpawn.Instance.EnemyCount;
        }
    }
}
