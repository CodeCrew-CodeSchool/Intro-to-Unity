using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalManagerScript : MonoBehaviour
{
    public enum GoalType {getToGoal, Collect, GetToScore};
    public GoalType setGoalType;
    public SceneManagerScript nextLevel;
    public int score = 0;
    public int GoalScore = 1; 
    public GameObject[] objCollect;
    public int numberToCollect;

    private int num;

    // Start is called before the first frame update
    void Start()
    {
        nextLevel = GetComponent<SceneManagerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        switch(setGoalType)
        {
            case GoalType.Collect:
                ArrayCheck();
                if(num >= numberToCollect)
                {
                    nextLevel.NextLevelFunction();
                }
            break;
            case GoalType.GetToScore:
                if(score >= GoalScore)
                {
                    nextLevel.NextLevelFunction();
                }
                break;

        }
    }

    public void ArrayCheck()
    {
        for(int i = 0; i < objCollect.Length; i++)
        {
            if (objCollect[i] == null)
            {
                num++;
            }
        }
    }

    public void AddScore(int add)
    {
        score += add;
    }


    public void FinishLevel()
    {
        nextLevel.NextLevelFunction();
    }
}
