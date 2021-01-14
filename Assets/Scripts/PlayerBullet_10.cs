using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet_10 : MonoBehaviour
{
    float speed;
    float corner;

    void Start()
    {
        Quaternion rotationZ = Quaternion.AngleAxis(-30, new Vector3(0, 0, -30));
        transform.rotation *= rotationZ;
        corner = -30f;
        speed = 8f;
    }


    void Update()
    {
        Vector2 position = transform.position;

        position = new Vector2(position.x + (corner / 5) * Time.deltaTime, position.y + speed * Time.deltaTime);

        transform.position = position;

        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        if (transform.position.y > max.y)
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EnemyShipTag") || (col.tag == "AsteroidTag"))
        {
            Destroy(gameObject);
        }
    }
}
