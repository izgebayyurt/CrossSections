using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;

public class SlideObject : MonoBehaviour
{
    private float _startingY;
    private float _startingZ;
    
    // Define a scale
    [Range(-10, 10)]
    public float positionRange;

    public List<GameObject> objectsToSlide;
    private int objectIdx;

    private void Start()
    {
        _startingY = transform.position.y;
        _startingZ = transform.position.z;
        objectIdx = 0;
        foreach (GameObject obj in objectsToSlide)
        {
            obj.SetActive(false);
        }
        objectsToSlide[0].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (positionRange < -10) positionRange = -10;
        if (positionRange > 10) positionRange = 10;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            if (objectIdx < objectsToSlide.Count - 1)
            {
                objectsToSlide[objectIdx].SetActive(false);
                objectIdx += 1;
                objectsToSlide[objectIdx].SetActive(true);
            }
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            if (objectIdx > 0)
            {
                objectsToSlide[objectIdx].SetActive(false);
                objectIdx -= 1;
                objectsToSlide[objectIdx].SetActive(true);
            }
        }
    }
    
    void LateUpdate()
    {
        Vector3 newPos = new Vector3(positionRange, _startingY, _startingZ);
        transform.position = newPos;
        foreach (GameObject obj in objectsToSlide)
        {
            obj.transform.position = newPos + Vector3.forward * -5;
        }
    }
}
