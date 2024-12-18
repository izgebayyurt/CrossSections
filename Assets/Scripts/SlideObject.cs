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
    public float positionMin;
    public float positionMax;
    private float _position;
    private float _normalizedPosition;

    public List<GameObject> objectsToSlide;
    private int objectIdx;

    private void Start()
    {
        _startingY = transform.localPosition.y;
        _startingZ = transform.localPosition.z;
        _position = transform.localPosition.x;
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
        UpdateButtonPosition();   

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
        Vector3 newPos = new Vector3(_position, _startingY, _startingZ);
        transform.localPosition = newPos;
        foreach (GameObject obj in objectsToSlide)
        {
            obj.transform.position = newPos + Vector3.forward * -5;
        }
    }

    void UpdateButtonPosition()
    {
        _position = transform.localPosition.x;
        _normalizedPosition = _position / positionMax;
        if (_position < positionMin) _position = positionMin;
        if (_position > positionMax) _position = positionMax;
    }
}
