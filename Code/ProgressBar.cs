using UnityEngine;
using System.Collections;

public class ProgressBar : MonoBehaviour {

    public GameObject Foreground;
    public float MaxValue;
    public float StartValue;

    private float _value;

    public float Value
    {
        get { return _value; }
        set
        {
            _value = Mathf.Clamp(value, 0, MaxValue);
            Foreground.transform.localScale = new Vector3(
                _value / MaxValue,
                1,
                1);
        }
    }

    public void Start()
    {
        Value = StartValue;
    }


}
