using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Bateria : MonoBehaviour
{

    public List<AudioClip> clave_beats;  // kick
    public List<AudioClip> filler_beats; // menos presente, sin saturacion //hithat cerrado
    public List<AudioClip> metric_beats; // snare,hithat


    private static List<string> rythm = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        getRythm("4/4","1/8");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void getRythm(string time_signature, string sub_division)
    {
        List<List<int>> rythm = RythmGenerator.Calculations(time_signature,sub_division);
        // metric,clave,clave_pattern,filler_pathern
        
        AssignSamples(rythm);
    }

    private static List<List<AudioClip>> AssignSamples(List<List<int>> rythm)
    {
        List<int> metric = rythm[0];
        List<int> clave  = rythm[0];
        List<int> filler = rythm[0];


        List<AudioClip> metric_ = new List<AudioClip>();
        List<AudioClip> clave_ = new List<AudioClip>();
        List<AudioClip> filler_ = new List<AudioClip>();

        /*foreach (int beat in metric)
        {
            if (beat == 1)
            {

            }
        }*/

        List<List<AudioClip>> result = new List<List<AudioClip>>();
        result.Add(metric_);
        result.Add(clave_);
        result.Add(filler_);
        return result;
    }
}
