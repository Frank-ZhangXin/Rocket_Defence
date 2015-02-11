using UnityEngine;
using System.Collections;

[RequireComponent(typeof(GameStage))]
public class MissileLauncher : MonoBehaviour {

    public float
        StartInSeconds,
        DurationInSeconds;

    public float
        NextLaunchMinSeconds,
        NextLaunchMaxSeconds;

    public GameObject Missile;

    public bool IsFinished { get; private set;}

    private float _timeToNextLaunch;
    private GameStage _stage;
    private int _missileCount;

    private void CalculateNextLaunch()
    {
        _timeToNextLaunch = Random.Range(NextLaunchMinSeconds, NextLaunchMaxSeconds);
    }

    public void Start()
    {
        _stage = GetComponent<GameStage>();
        CalculateNextLaunch();
    }

    public void Update()
    {
        if (!_stage.IsRunning)
            return;

        if (StartInSeconds > _stage.StageTime)
            return;

        if (StartInSeconds + DurationInSeconds < _stage.StageTime)
        {
            if (_missileCount == 0)
                IsFinished = true;
            return;
        }
            


        if (_timeToNextLaunch > 0)
        {
            _timeToNextLaunch -= Time.deltaTime;
            return;
        }

        var missile = (GameObject)Instantiate(Missile, transform.position, transform.rotation);
        missile.GetComponent<BasicMissile>().Init(this);
        _missileCount++;

        CalculateNextLaunch();
    }

    public void DestroyMissile(BasicMissile missile)
    {
        _missileCount--;
    }
}
