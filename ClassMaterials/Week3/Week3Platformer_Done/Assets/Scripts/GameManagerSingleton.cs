using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManagerSingleton : Singleton<GameManagerSingleton>
{

    [SerializeField] GameObject playerPrefab;

    GameObject _player = null;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        _player = Instantiate(playerPrefab);
        KillCount = 0;
    }

    // Update is called once per frame
    void Update()
    {
        /*if (_player == null)
            if (_lives > 0)//intermediate thing to before we implemented PlayerDeath()
                _player = Instantiate(playerPrefab);
        */
    }

    public GameObject GetPlayer()
    {
        return _player;
    }

    [SerializeField] int _killCount;
    //make sure to use this property when we add it
    public int KillCount
    {
        get => _killCount;
        set
        {
            _killCount = value;
            //update the score UI
            ScoreSetter scoreSetter = FindAnyObjectByType<ScoreSetter>();
            if(scoreSetter != null)
            {
                scoreSetter.UpdateScore(KillCount);
            }
        }
    }


    [SerializeField] int _lives = 3;
    public int Lives
    {
        get => _lives;
        set
        {
            _lives = value;
            //update the lives UI
            LivesSetter livesSetter = FindAnyObjectByType<LivesSetter>();
            if (livesSetter != null)
            {
                livesSetter.UpdateLives(Lives);
            }
        }
    }

    public void IncrementKillCount()
    {
        KillCount += 1;
        Debug.LogFormat("Killed {0} total so far", _killCount);
    }

    public void PlayerDeath()
    {
        _player.GetComponent<PlayerInfo>().Die();
        Lives -= 1;
        if (_lives > 0)
        {
            _player = Instantiate(playerPrefab);
        }
        else
        {
            Debug.LogFormat("Game Over. Score = {0}", _killCount);
        }
    }

    ////Scene Management Code
    [SerializeField] string[] levels = new string[] { "Scene1", "Scene2" };
    int _sceneIndex = 0;

    public void LoadNextLevel()
    {
        _sceneIndex++;
        if (_sceneIndex > levels.Length - 1)
            _sceneIndex = 0;
        SceneManager.LoadScene(levels[_sceneIndex]);
        //make the player spawn after the scene has loaded
        StartCoroutine(SpawnPlayerCoroutine());
    }

    IEnumerator SpawnPlayerCoroutine()
    {
        //wait one frame, then spawn the player once the new scene has actually loaded.
        //this is a gross solution. I bet we can make clever use of our EventBus to do this nicer...
        while (_player != null)//can comment this while out and it will work *most* of the time
            yield return null;
        _player = Instantiate(playerPrefab);
    }

}
