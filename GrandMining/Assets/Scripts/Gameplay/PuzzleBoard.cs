using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleBoard : MonoBehaviour
{
    public int width, height;
    public GameObject gemBackgroundPrefab;
    private GemBackground [,] allBackgrounds;
    // Start is called before the first frame update
    void Start()
    {
        allBackgrounds = new GemBackground[width, height];
        boardSetup();
    }
    private void boardSetup()
    {
        for (int i = 0; i < width; i++)
        {
            for(int j = 0; j < height; j++)
            {
                Vector2 TemporaryPosition = new Vector2(i, j);
                GameObject gemBackground = Instantiate(gemBackgroundPrefab, TemporaryPosition, Quaternion.identity) as GameObject;
                gemBackground.transform.parent = this.transform;
                //Instantiate(gemBackgroundPrefab, TemporaryPosition, Quaternion.identity);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
