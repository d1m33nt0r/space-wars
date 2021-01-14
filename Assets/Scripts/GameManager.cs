using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // reference to our game objects
    public GameObject playButton;
    public GameObject playerShip;
    public GameObject enemySpawner; // reference to our enemy spawner
    public GameObject GameOverGO;
    public GameObject scoreUITextGO;
    public GameObject TimeCounterGO;
    public GameObject ShootButton;
    public GameObject MegaShootButton;
    public GameObject joyStick;
    public GameObject exitButton;
    public GameObject asteroidSpawner;

    public GameObject BestScoreUI;

    public GameObject bestScoreUITextGO;

    public AudioSource gameplaySound;


    public enum GameManagerState
    {
        Opening,
        Gameplay,
        GameOver,
    }

    GameManagerState GMState;


    // Start is called before the first frame update
    void Start()
    {
        

        gameplaySound = GameObject.FindGameObjectWithTag("gameplaySound").GetComponent<AudioSource>();

        GMState = GameManagerState.Opening;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // function to update the GameManager state
    void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:


                //hide gameover
                GameOverGO.SetActive(false);

                //set play button visible(active)
                playButton.SetActive(true);

                exitButton.SetActive(true);

                break;
            case GameManagerState.Gameplay:



                joyStick.GetComponent<MobileController>().Centered();
                gameplaySound.Play();

                scoreUITextGO.GetComponent<GameScore>().Score = 0;

                //hide play button on the game play state
                playButton.SetActive(false);
                exitButton.SetActive(false);
                BestScoreUI.SetActive(false);

                joyStick.SetActive(false);

                ShootButton.SetActive(true);

                //set the player visible (active) and init the player lives
                playerShip.GetComponent<PlayerControl>().Init();

                asteroidSpawner.GetComponent<AsteroidSpawner>().ScheduleAsteroidSpawner();

                //start enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().ScheduleEnemySpawner();

                TimeCounterGO.GetComponent<TimeCounter>().StartTimeCounter();

                break;
            case GameManagerState.GameOver:

                BestScoreUI.SetActive(true);

                bestScoreUITextGO.GetComponent<BestScore>().SetBestScore();

                gameplaySound.Stop();

                TimeCounterGO.GetComponent<TimeCounter>().StopTimeCounter();

                // stop enemy spawner
                enemySpawner.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();



                // Display GAMEOVER
                GameOverGO.SetActive(true);
                ShootButton.SetActive(false);
                MegaShootButton.SetActive(false);

                joyStick.SetActive(false);

                // change game manager state to opening after 8 seconds
                Invoke("ChangeToOpeningState", 8f);

                break;
        }
    }

    // function to set the game manager state
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    //Our Play button will call this function
    //when the user click the button.
    public void StartGamePlay()
    {
        GMState = GameManagerState.Gameplay;
        UpdateGameManagerState();
    }

    public void Quit()
    {
            Application.Quit();
    }

    public void ChangeToOpeningState()
    {
        SetGameManagerState(GameManagerState.Opening);
    }



}
