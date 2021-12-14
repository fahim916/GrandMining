using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinPhase : MonoBehaviour
{
    private float timeInterval;
    // Start is called before the first frame update
    void Start()
    {
        Manger.DigPhase += Show;
    }

    private void Show()
    {
        StartCoroutine("Display");
    }

    private IEnumerator Display()
    {
        timeInterval = 2;
        Debug.Log("Start showing buying options");
        yield return new WaitForSeconds(timeInterval);
    }

    private void OnDestroy()
    {
        Manger.DigPhase -= Show;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
