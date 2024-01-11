using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform player;

    public Vector3 offset;
    private float zoom = -1f;

    public bool FellowOnX = true;
    public bool FellowOnY = true;

    

    private float x_position = 0f;
    private float y_position = 0f;
    // Start is called before the first frame update
    void Start()
    {
        if(player == null)
        {
             x_position = 0f;
             y_position = 0f;
        }
        x_position = player.position.x;
        y_position = player.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        CamFunction();
    }

    public void CamFunction()
    {
        if (FellowOnX)
        {
            x_position = player.position.x;
        }
        else
        {
            x_position = 0f;
        }

        if (FellowOnY)
        {
            y_position = player.position.y;
        }
        else
        {
            y_position = 0f;
        }

        transform.position = new Vector3 (x_position, y_position, zoom) + offset ;
        
    }
    

}
