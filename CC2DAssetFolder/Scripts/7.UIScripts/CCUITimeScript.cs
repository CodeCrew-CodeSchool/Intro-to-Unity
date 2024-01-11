using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Events;

public class CCUITimeScript : MonoBehaviour
{
    public enum timeStates {countup, countdown, none};
    public timeStates currentState;

    public float time;
    public float speedOfTimer = 1.0f;

    public TextMeshProUGUI UIText;
    public string beforeText;
    public string afterText;

    public bool isActive;


    public bool StartOnScene = false;

    public UnityEvent timeDone;

    // Start is called before the first frame update
    void Start()
    {
        if(StartOnScene)
        {
            StartCoroutine(startTimer());
        }
    }



    // Update is called once per frame
    void Update()
    {
        UIText.text = beforeText + " " + time.ToString() + " " + afterText;

        if(time <= 0)
        {
            timeDone.Invoke();
        }
    }

    

    IEnumerator startTimer()
    {
        while(isActive)
        {
            yield return new WaitForSeconds(speedOfTimer);
            if (currentState == timeStates.countup)
            {
                time++;
            }
            else if (currentState == timeStates.countdown)
            {
                time--;
            }
        }
    }


}
