using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    //UI things -----------------------------
    public InputField tempoInput;
    public Dropdown metricDropdown;
    public Dropdown subDivDropdown;
    public Text claveText;
    public Text fillerText;
    //UI things -----------------------------

    public string time_signature; // "4/4"; // { "3/4", "4/4" };
    public string sub_division; // { "1/8", "1/16" };

    //Audios -------------------------------------------------------------
    public double bpm; //120.0F;
    private double interval = 0.0f;
    private double sub_interval = 0.0f;

    //Samples -------------------------------------------------------------
    public AudioSource audioSource;
    public AudioClip tick;
    public AudioClip metric;
    public AudioClip clave;
    public AudioClip filler;

    List<List<int>> rythm = new List<List<int>>();

    List<int> metric_pattern = new List<int>();
    List<int> clave_pattern = new List<int>();
    List<int> filler_pattern = new List<int>();

    //Metronome -----------------------------
    public bool playMetronome = true;
    public int counterControl = 0;
    public int counter = 0;



    void Start()
    {
       
        
    }

    public void Update()
    {
        

    }

    public void GenerateRythm()
    {

        rythm = RythmGenerator.Calculations(time_signature, sub_division);
        metric_pattern = rythm[0];
        clave_pattern = rythm[1];
        filler_pattern = rythm[2];

        Debug.Log("metric_pattern: " + string.Join(",", metric_pattern));
        Debug.Log("clave_pattern:  " + string.Join(",", clave_pattern));
        Debug.Log("fill_pattern:   " + string.Join(",", filler_pattern));

        //UI ------------
        bpm = 100f; // Double.Parse(tempoInput.text);
        time_signature = metricDropdown.options[metricDropdown.value].text;
        sub_division = subDivDropdown.options[metricDropdown.value].text;
        claveText.text = "" + string.Join(",", clave_pattern);
        fillerText.text = "" + string.Join(",", filler_pattern);

        Debug.Log("bpm"+bpm);
        Debug.Log("tsg" + time_signature);
        Debug.Log("sub" + sub_division);
        //-- ------------
    }


    private bool EvalFillerPattern()
    {
        int internMetric = (int)time_signature[0];
        //Debug.Log("asdfasdfaL: "+ time_signature[0]);
        int indexx = counter % internMetric;
        return filler_pattern[counter] == 1 ? true : false;
    }

    private bool EvalClavePattern()
    {
        int internMetric = (int)time_signature[0];
        int indexx = counter % internMetric;
        return clave_pattern[counter] == 1 ? true : false;
    }

    private bool EvalMetricPattern()
    {
        int internMetric = (int)time_signature[0];
        int indexx = counter % internMetric;
        return metric_pattern[counter] == 1 ? true : false;
    }

    public void PlayRythm()
    {
        StartCoroutine(PlaySamples());
    }

    public void StopRythmCorutine()
    {
        StopCoroutine(PlaySamples());
    }

    IEnumerator PlaySamples()
    {

        while(Time.time < 1000 && playMetronome)
        {
            //audioSource.PlayOneShot(metric, 0.5f);
            counterControl++;
            //Debug.Log("counterControl:"+counterControl);
            //Debug.Log("counter: "+counter);
            //Debug.Log("clave beat: "+EvalClavePattern());

            audioSource.PlayOneShot(tick, 0.5f);
            if (counterControl % 2 == 0)
            {
                Debug.Log("clave beat: " + EvalClavePattern());

                //001
                if (EvalFillerPattern() == false && EvalClavePattern() == false && EvalMetricPattern() == true )
                {
                    //Debug.Log("clave");
                    audioSource.PlayOneShot(metric, 0.5f);
                }
                //010
                else if (EvalFillerPattern() == false && EvalClavePattern() == true && EvalMetricPattern() == false)
                {
                    //Debug.Log("clave");
                    audioSource.PlayOneShot(clave, 0.5f);
                }
                //011
                else if (EvalFillerPattern() == false && EvalClavePattern() == true && EvalMetricPattern() == true)
                {
                    //Debug.Log("filler");
                    audioSource.PlayOneShot(metric, 0.5f);
                    audioSource.PlayOneShot(clave, 0.5f);
                }
                //100
                else if (EvalFillerPattern() == true && EvalClavePattern() == false && EvalMetricPattern() == false)
                {
                    //Debug.Log("filler");
                    audioSource.PlayOneShot(filler, 0.5f);
                }
                //101
                else if (EvalFillerPattern() == true && EvalClavePattern() == false && EvalMetricPattern() == true)
                {
                    //Debug.Log("filler");
                    audioSource.PlayOneShot(metric, 0.5f);
                    audioSource.PlayOneShot(filler, 0.5f);
                }
                //110
                else if (EvalFillerPattern() == true && EvalClavePattern() == true && EvalMetricPattern() == false)
                {
                    //Debug.Log("filler");
                    audioSource.PlayOneShot(filler, 0.5f);
                    audioSource.PlayOneShot(clave, 0.5f);
                }
                //111
                else if (EvalFillerPattern() == true && EvalClavePattern() == true && EvalMetricPattern() == true)
                {
                    //Debug.Log("clave");
                    audioSource.PlayOneShot(metric, 0.5f);
                    audioSource.PlayOneShot(clave, 0.5f);
                    audioSource.PlayOneShot(filler, 0.5f);
                }


                counter++;
                if (time_signature.StartsWith("4"))
                {
                    if (counter == 16)
                    {
                        counter = 0;
                    }
                }
                else if (time_signature.StartsWith("3"))
                {
                    if (counter == 12)
                    {
                        counter = 0;
                    }
                }
            }

            interval = 60.0f / bpm;
            sub_interval = interval / 2;
            yield return new WaitForSecondsRealtime((float)sub_interval);
        }
    }


}
