using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackGroundScript : MonoBehaviour
{
    public Vector2 XYspeed = new Vector2(1.0f, 1.0f);

    private Renderer _renderer;
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 offset =new Vector2(Time.time * XYspeed.x, Time.time * XYspeed.y);
        _renderer.material.mainTextureOffset = offset;
    }
}
