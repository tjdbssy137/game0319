using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class EmemyMove : MonoBehaviour
{
    private GameObject Player;
    private float speed = 0.8f;

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
            int random = Random.Range(0, 6);
            switch(random)
            { 
                case 3:
                    Instantiate(dropItem[0], this.transform.position, this.transform.rotation);//���� �ڸ����� �ȿ����̰�..
                    break;
                case 4:
                    Instantiate(dropItem[1], this.transform.position, this.transform.rotation);
                    break;
                case 5:
                    Instantiate(dropItem[2], this.transform.position, this.transform.rotation);
                    break;
                default:
                    break;
            }
            Destroy(this.gameObject);
            --EnemySpawn.Instance.EnemyCount;
        }
    }
}
