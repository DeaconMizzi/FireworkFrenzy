using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableDelay : MonoBehaviour
{
   public GameObject objectToEnable;
   public GameObject objectToEnable2;
   public GameObject objectToEnable3;
   public float delay1 = 2f;
   public float delay2 = 2f;
   public float delay3 = 2f;
   
    // Adjust delay time as needed

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnableObjectDelayed1());
        StartCoroutine(EnableObjectDelayed2());
        StartCoroutine(EnableObjectDelayed3());
    }

    IEnumerator EnableObjectDelayed1()
    {
        yield return new WaitForSeconds(delay1);

        if (objectToEnable != null)
        {
            objectToEnable.SetActive(true); // Enable the GameObject after the delay
        }
        else
        {
            Debug.LogWarning("No object assigned to enable!");
        }
    }

    IEnumerator EnableObjectDelayed2()
    {
        yield return new WaitForSeconds(delay2);

        if (objectToEnable2 != null)
        {
            objectToEnable2.SetActive(true); // Enable the GameObject after the delay
        }
        else
        {
            Debug.LogWarning("No object assigned to enable!");
        }
    }

    IEnumerator EnableObjectDelayed3()
    {
        yield return new WaitForSeconds(delay3);

        if (objectToEnable3 != null)
        {
            objectToEnable3.SetActive(true); // Enable the GameObject after the delay
        }
        else
        {
            Debug.LogWarning("No object assigned to enable!");
        }
    }
}
