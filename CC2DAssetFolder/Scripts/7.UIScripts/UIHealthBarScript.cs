using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static AI_goToObject;
using static UnityEngine.GraphicsBuffer;

public class UIHealthBarScript : MonoBehaviour
{
    public enum useToFindObject { useGameObject, useTags, useName };
    public useToFindObject useHealthSettings = useToFindObject.useGameObject;
    public string targetTagOrName = "Player";

    public Image healthBar;
    public HealthScript playerHealth;
    // Start is called before the first frame update
    void Start()
    {
        GetTarget();
    }

    // Update is called once per frame
    void Update()
    {
        healthBar.fillAmount = playerHealth.currHealth/ playerHealth.maxHealth;
    }

    public void GetTarget()
    {
        switch (useHealthSettings)
        {
            case useToFindObject.useGameObject:

                break;
            case useToFindObject.useTags:
                playerHealth = GameObject.FindWithTag(targetTagOrName).GetComponent<HealthScript>();
                break;
            case useToFindObject.useName:
                playerHealth = GameObject.Find(targetTagOrName).GetComponent<HealthScript>();
                break;
        }
    }
}
