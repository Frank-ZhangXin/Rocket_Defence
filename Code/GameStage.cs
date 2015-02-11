using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameStage : MonoBehaviour {

    public Color BackgroundColor;

    public float StageTime { get; private set; }
    public bool IsRunning { get; private set; }

    private List<MissileLauncher> _launchers;

    public void Start()
    {
        _launchers = new List<MissileLauncher>(GetComponents<MissileLauncher>());
    }

    public void Update()
    {
        if (!IsRunning)
            return;

        if (_launchers.All(l => l.IsFinished))
        {
            IsRunning = false;
            return;
        }

        StageTime += Time.deltaTime;
    }

    public void StartStage()
    {
        StageTime = 0;
        IsRunning = true;
    }
}
