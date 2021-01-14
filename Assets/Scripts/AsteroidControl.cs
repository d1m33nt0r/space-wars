using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidControl : MonoBehaviour
{
    public GameObject ExplosionGO;
    public GameObject HealthGO;
    public GameObject FireBonus;
    float speed;

    Vector2 _direction;

    bool isHealth = false;
    // Start is called before the first frame update
    void Start()
    {
        speed = 3.5f;
    }

    public void SetDirection(Vector2 direction)
    {
        _direction = direction.normalized;
    }

    // Update is called once per frame
    void Update()
    {

        //   Vector2 pos = transform.position;

        //    pos = new Vector2(pos.x, pos.y - speed * Time.deltaTime);

        //     transform.position = pos;

        // get the bullet’s current position
        Vector2 position = transform.position;

        // compute the bullet’s new position
        position += _direction * speed * Time.deltaTime;

        // Update the bullet’s position
        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "PlayerBulletTag"))
        {
            Destroy(gameObject);
            PlayExplosion();
            int a = Random.Range(1, 30);
            if (!isHealth && a == 17)
            {
                HealthCreate();
            }
            if (!isHealth && a == 9)
            {
                FireBonusCreate();
            }


        }
        if (collision.tag == "EnemyBulletTag")
        {
            PlayExplosion();

            Destroy(gameObject);
        }
    }

    void PlayExplosion()
    {
        Vector2 pos = transform.position;

        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = pos;
    }

    void HealthCreate()
    {
        Vector2 pos = transform.position;

        GameObject health = (GameObject)Instantiate(HealthGO);

        health.transform.position = pos;

        isHealth = true;
    }

    void FireBonusCreate()
    {
        Vector2 pos = transform.position;

        GameObject fireBonus = (GameObject)Instantiate(FireBonus);

        fireBonus.transform.position = pos;

        isHealth = true;
    }

}
