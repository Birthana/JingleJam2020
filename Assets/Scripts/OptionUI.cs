using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OptionUI : MonoBehaviour
{
    public GameObject timeBar;
    public Transform startPos;
    public Transform endPos;

    private void Start()
    {
        timeBar.transform.localScale = new Vector3(
            0, 
            timeBar.transform.localScale.y,
            timeBar.transform.localScale.z
        );
    }

    private void OnEnable()
    {
        GetComponent<Option>().OnOptionChange += Display;
    }

    private void OnDisable()
    {
        GetComponent<Option>().OnOptionChange -= Display;
    }

    public void Display(int currentTime, int maxTime)
    {
        float t = (float)currentTime / maxTime;
        float x = Mathf.Lerp(startPos.position.x, endPos.position.x, t);
        timeBar.transform.localScale = new Vector3(
            (x - startPos.transform.position.x) / (endPos.transform.position.x - startPos.transform.position.x), 
            timeBar.transform.localScale.y, 
            timeBar.transform.localScale.z
        );
    }
}
