using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashAmountPrefab : MonoBehaviour
{
    private float destroyTime = 2f;
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, destroyTime);
    }

}
