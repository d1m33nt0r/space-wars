using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyControl : MonoBehaviour
{

    public GameObject scoreUITextGO;
    public GameObject ExplosionGO;
    public GameObject AsteroidGO;

    bool isAdded = false;
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = 2f;

        scoreUITextGO = GameObject.FindGameObjectWithTag("ScoreTextTag");
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x, position.y - speed * Time.deltaTime);

        transform.position = position;

        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

        if (transform.position.y < min.y)
        {
            Destroy(gameObject);
        }

    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "PlayerShipTag") || (collision.tag == "PlayerBulletTag"))
        {
            PlayExplosion();

            if (!isAdded)
            { 
                AddPoints();
            }
            Destroy(gameObject);
        }

        if (collision.tag == "AsteroidTag")
        {
            PlayExplosion();
            Destroy(gameObject);
        }
    }

    void AddPoints()
    {
        scoreUITextGO.GetComponent<GameScore>().Score += 100;

        isAdded = true;
    }

    void PlayExplosion()
    {
        Vector2 pos = transform.position;

        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = pos;
    }
}
