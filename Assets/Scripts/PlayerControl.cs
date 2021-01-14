using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    int n = 0;
    public GameObject GameManagerGO;
    public GameObject ButtonShoot;
    public GameObject ButtonMegaShoot;
    public GameObject PlayerBulletGO;
    public GameObject BulletR15;
    public GameObject BulletR30;

    public GameObject BulletL15;
    public GameObject BulletL30;

    public GameObject Position_10;
    public GameObject Position_05;
    public GameObject Position00;
    public GameObject Position05;
    public GameObject Position10;

    public GameObject ExplosionGO;
    public float speed;

    public AudioSource audio;

    public Text LivesUIText;

    private MobileController mContr;
    bool ChangedShoot;
    const int maxLives = 3;
    int lives;

    float accelStartY;

    int timer;

    public void Init()
    {
        lives = maxLives;

        LivesUIText.text = lives.ToString();

        transform.position = new Vector2(0, 0);

        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        accelStartY = Input.acceleration.y;

        audio = GetComponent<AudioSource>();

        mContr = GameObject.FindGameObjectWithTag("joyStick").GetComponent<MobileController>();
    }

    // Update is called once per frame
    void Update()
    {

        //float x = Input.GetAxisRaw("Horizontal");
        //float y = Input.GetAxisRaw("Vertical");
        float x = mContr.Horizontal();
        float y = mContr.Vertical();

        //float x = Input.acceleration.x;
        //float y = Input.acceleration.y - accelStartY/2;

        Vector2 direction = new Vector2(x, y);

        if (direction.sqrMagnitude > 1)
        {
            direction.Normalize();
        }

        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0)); // устанавливаем нижнюю левую точку экрана
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1)); // устанавливаем правую верхнюю точку экрана

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        // Получаем текущее положение корабля игрока
        Vector2 pos = transform.position;

        // Прощет нового положения корабля
        pos += direction * speed * Time.deltaTime;

        // Ограничиваем движение корабля за пределы экрана
        pos.x = Mathf.Clamp(pos.x, min.x, max.x);
        pos.y = Mathf.Clamp(pos.y, min.y, max.y);

        // Обновляем положение корабля игрока
        transform.position = pos;
    }

    public void MegaShoot()
    {
        audio.Play();

        GameObject bullet00 = (GameObject)Instantiate(PlayerBulletGO);
        bullet00.transform.position = Position00.transform.position;
        GameObject bullet05 = (GameObject)Instantiate(BulletR15);
        bullet05.transform.position = Position05.transform.position;
        GameObject bullet10 = (GameObject)Instantiate(BulletR30);
        bullet10.transform.position = Position10.transform.position;
        GameObject bullet_05 = (GameObject)Instantiate(BulletL15);
        bullet_05.transform.position = Position_05.transform.position;
        GameObject bullet_10 = (GameObject)Instantiate(BulletL30);
        bullet_10.transform.position = Position_10.transform.position;

    }

    public void Shoot()
    {
        audio.Play();

        GameObject bullet01 = (GameObject)Instantiate(PlayerBulletGO);
        bullet01.transform.position = Position_10.transform.position;
        GameObject bullet02 = (GameObject)Instantiate(PlayerBulletGO);
        bullet02.transform.position = Position10.transform.position;
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if ((collision.tag == "EnemyShipTag") || (collision.tag == "EnemyBulletTag") || (collision.tag == "AsteroidTag"))
        {
            PlayExplosion();

            lives--;

            LivesUIText.text = lives.ToString();

            if (lives == 0)
            {
                GameManagerGO.GetComponent<GameManager>().SetGameManagerState(GameManager.GameManagerState.GameOver);
                ChangeFire();
                ButtonShoot.SetActive(false);
                gameObject.SetActive(false);
            }
        }

        if (collision.tag == "HealthTag")
        {
            lives+=1;
            LivesUIText.text = lives.ToString();
        }
        if (collision.tag == "FireBonusTag")
        {
            BonusStart();
        }
    }

    void PlayExplosion()
    {
        Vector2 pos = transform.position;

        GameObject explosion = (GameObject)Instantiate(ExplosionGO);

        explosion.transform.position = pos;
    }

    public void BonusStart()
    {
        if (!ChangedShoot)
        {
            timer += 20;
            ChangeMegaFire();
            ReStartTimer();
        }
        if (ChangedShoot)
        {
            ReStartTimer();
            ChangeMegaFire();
        }
        InvokeRepeating("timerDecrement", 1, 1);
        //if (n == 0)
        //{ 

        //}
        //n++;

    }

    void timerDecrement()
    {
        if (timer > 0)
        {
            timer--;
        }
        
        if (timer == 0)
        {
            ChangeFire();
            CancelInvoke();
        }
        Debug.Log(timer);
    }

    public void ReStartTimer()
    {
        timer = 20;
    }

    void ChangeMegaFire()
    {
        ButtonShoot.SetActive(false);
        ButtonMegaShoot.SetActive(true);
        ChangedShoot = false;
    }

    void ChangeFire()
    {
        ButtonShoot.SetActive(true);
        ButtonMegaShoot.SetActive(false);
        ChangedShoot = true;
    }
}
