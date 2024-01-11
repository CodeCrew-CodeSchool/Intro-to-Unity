using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneManagerScript : MonoBehaviour
{
   

    public enum Usable {restartScene ,useString, useIndex};
    public enum nextLevelSetting{JustNextIndex ,nextLevelIndex, nextLevelName};


    public string fileRouteString;

    public bool restartLevelOnDeath = true;
    public Usable useOnDeath = Usable.useString;
    public string deadSceneName;
    public int deadSceneIndex;

    public nextLevelSetting howToMoveOn = nextLevelSetting.JustNextIndex; 
    public string nextSceneName;
    public int nextSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SceneOfDeath()
    {
        if(useOnDeath ==Usable.restartScene)
        {
            SceneManager.LoadScene(fileRouteString + SceneManager.GetActiveScene().name);
        }
        else if(useOnDeath == Usable.useIndex)
        {
            SceneManager.LoadScene(deadSceneIndex);
        }
        else
        {
            SceneManager.LoadScene(deadSceneName);
        }
        
    }

    public void NextLevelFunction()
    {
            if(howToMoveOn == nextLevelSetting.JustNextIndex)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else if(howToMoveOn == nextLevelSetting.nextLevelIndex)
            {
                SceneManager.LoadScene(nextSceneIndex);
            }
            else
            {
                SceneManager.LoadScene(nextSceneName);
            }
    }
}
