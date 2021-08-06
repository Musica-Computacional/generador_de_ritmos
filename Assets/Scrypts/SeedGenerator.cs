using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeedGenerator : MonoBehaviour
{
    public string string_seed = "seed";
    public bool use_string_seed = true;
    public bool random_seed = false;
    public int seed = 1234;
    public GameObject TextInput;

    /*public void GenerateSeed()
    {
        if (use_string_seed)
        {
            seed = string_seed.GetHashCode();
        }

        if (random_seed)
        {
            seed = Random.Range(1, 99999);
        }

        Random.InitState(seed);
        // return seed;
    }*/


    void Awake()
    {
        if (use_string_seed)
        {
            seed = string_seed.GetHashCode();
        }

        if (random_seed)
        {
            seed = Random.Range(1, 99999);
        }

        TextInput.GetComponent<Text>().text = "" + seed;

        Random.InitState(seed);
        // return seed;
    }

    void Start()
    {
        Debug.Log("yeah");
        Debug.Log(seed);
    }

    // Update is called once per frame
    void Update()
    {

    }

}
