using UnityEngine;
using System.Collections;

public class LevelManager : MonoBehaviour {

    public GameStage[] Stages;

    public GUIText Text;

    private GameStage _currentStage;
    private int _stageIndex;
    private bool _isStartingStage;
    private bool _isChangingColor;

    private Color _toColor;
    private float _colorLerp;

    public void Start()
    {
        if (Stages.Length == 0)
            Debug.Log("No stages founds!");

        StartCoroutine(StartStage().GetEnumerator());
    }

    public void Update()
    {
        if (_isChangingColor)
        {
            _colorLerp += Time.deltaTime * 0.01f;
            Camera.main.backgroundColor = Color.Lerp(Camera.main.backgroundColor, _toColor, _colorLerp);

            if (_colorLerp > 1)
                _isChangingColor = false;
        }

        if (_currentStage == null)
            return;

        if (_currentStage.IsRunning || _isStartingStage)
            return;
        _stageIndex++;
        if (_stageIndex >= Stages.Length)
        {

            /// Bug here!
            Text.gameObject.SetActive(true);
            Text.text = "You won!";
            Text.gameObject.SetActive(false);
            return;
        }

        StartCoroutine(StartStage().GetEnumerator());
    }

    private IEnumerable StartStage()
    {
        _isStartingStage = true;
        

        Text.gameObject.SetActive(true);

        _currentStage = Stages[_stageIndex];
        _isChangingColor = true;
        _toColor = _currentStage.BackgroundColor;
        _colorLerp = 0;

        Text.text = "Stage " + (_stageIndex + 1) + "...";

        yield return new WaitForSeconds(1.5f);

        Text.text = "FIGHT!";

        yield return new WaitForSeconds(1.5f);

        Text.gameObject.SetActive(false);

        _currentStage.StartStage();

        _isStartingStage = false;
    }
}
