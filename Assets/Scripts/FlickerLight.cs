using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlickerLight : MonoBehaviour
{
    Light light;
    float minSpd = 0.01f;
    float maxSpd = 0.1f;
    float minIntnst = 0.1f;
    float maxIntnst = 1f; 
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        StartCoroutine(run());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator run(){
        while(true){
            light.enabled = true;
            light.intensity = Random.Range(minIntnst, maxIntnst);
            yield return new WaitForSeconds(Random.Range(minSpd, maxSpd));
            light.enabled = false;
            yield return new WaitForSeconds(Random.Range(minSpd, maxSpd));
        }
    }
}
