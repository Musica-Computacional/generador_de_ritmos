using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public string time_signature = "4/4"; // { "3/4", "4/4" };
    public string sub_division; // { "1/8", "1/16" };

    //audios -------------------------------------------------------------
    public double bpm = 120.0F;
    private double interval = 0.0f;
    private double sub_interval = 0.0f;

    //samples -------------------------------------------------------------
    public AudioSource audioSource;
    public AudioClip tick;

    public AudioClip metric;
    public AudioClip clave;
    public AudioClip filler;

    List<List<int>> rythm = new List<List<int>>();

    List<int> metric_pattern = new List<int>();
    List<int> clave_pattern = new List<int>();
    List<int> filler_pattern = new List<int>();

    //metronome -----------------------------
    public bool playMetronome = true;
    public int counterControl = 0;
    public int counter = 0;


    void Start()
    {
        rythm = RythmGenerator.Calculations(time_signature, sub_division);
        metric_pattern = rythm[0];
        clave_pattern = rythm[1];
        filler_pattern = rythm[2];

        Debug.Log("metric_pattern: " + string.Join(",", metric_pattern));
        Debug.Log("clave_pattern:  " + string.Join(",", clave_pattern));
        Debug.Log("fill_pattern:   " + string.Join(",", filler_pattern));

        InitializeCorutine();

    }

    public void Update()
    {
        //double tickkk = metronome.GetNextTickTime();
        //Debug.Log("next tick time: "+tickkk);
    }

    private bool EvalFillerMetric()
    {
        int internMetric = (int)time_signature[0];
        int indexx = counter % internMetric;
        return filler_pattern[indexx] == 1 ? true : false;
    }

    private bool EvalMainMetric()
    {
        int internMetric = (int)time_signature[0];
        int indexx = counter % internMetric;
        return clave_pattern[indexx] == 1 ? true : false;
    }

    public void InitializeCorutine()
    {
        StartCoroutine(PlaySamples());
    }

    IEnumerator PlaySamples()
    {

        while(Time.time < 100 && playMetronome)
        {
            audioSource.PlayOneShot(metric, 0.5f);
            counterControl++;

            if (counterControl % 2 == 0)
            {
                if (EvalMainMetric() == true && EvalFillerMetric() == false)
                {
                    Debug.Log("clave");
                    audioSource.PlayOneShot(clave,0.5f);
                }
                else if (EvalMainMetric() == false && EvalFillerMetric() == true)
                {
                    Debug.Log("filler");
                    audioSource.PlayOneShot(filler, 0.5f);
                }
                if (EvalMainMetric() == true && EvalFillerMetric() == true)
                {
                    Debug.Log("clave");
                    audioSource.PlayOneShot(clave, 0.5f);
                    Debug.Log("filler");
                    audioSource.PlayOneShot(filler, 0.5f);
                }
                counter++;
            }

            interval = 60.0f / bpm;
            sub_interval = interval / 2;
            yield return new WaitForSecondsRealtime((float)sub_interval);
        }
    }


}
