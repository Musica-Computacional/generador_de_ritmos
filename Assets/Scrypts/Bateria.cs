using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bateria : MonoBehaviour
{

    public List<AudioClip> clave_beats;
    public List<AudioClip> filler_beats;
    public List<AudioClip> metric_beats;


    private static List<string> rythm = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        //getRythm();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void getRythm(string time_signature, string sub_division)
    {
        string r = RythmGenerator.Calculations(time_signature,sub_division);
        // clave,clave_pattern,filler_pathern
        rythm = r.Split('_').ToList();
        AssignSamples(rythm);
    }

    static void AssignSamples(List<string> rythm)
    {
        List<string> result = new List<string>();
        return result;
    }
}
