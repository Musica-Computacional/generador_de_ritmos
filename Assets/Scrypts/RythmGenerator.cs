using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RythmGenerator : MonoBehaviour
{
    public string[] time_signatures = new string[] {"3/4","4/4"};
    public double tempo = 60.0;
    
    public GameObject text_ran_gen_num;
    public int num;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    /*void Awake()
    {
        seed = Random.Range(1, 99999);
        Random.InitState(seed)
    }*/

    public void UpdateText()
    {
        num = Random.Range(1, 99999);
        text_ran_gen_num.GetComponent<Text>().text = "" + num;
        // return seed;
    }


    private int[] clave_generator(string time_signature)
    {
        int[] clave = new int[] { 1, 1, 1, 1, 1, 1 };
        return clave;
    }

    private int[] fill_generator(string time_signature)
    {
        int[] filler = new int[] { 1, 1, 1, 1, 1, 1 };
        return filler;
    }
}
