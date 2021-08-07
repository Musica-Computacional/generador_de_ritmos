using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class RythmGenerator : MonoBehaviour
{

    public string time_signature; //{ "3/4", "4/4" };
    public string sub_division; // { "1/8", "1/16" };
    public double tempo = 60.0;

    public GameObject text_ran_gen_num;
    public int num;

    // Start is called before the first frame update
    void Start()
    {
        //DebugMethod();
        List<string> actual_clave = ClaveGenerator(time_signature);
        Debug.Log("actual_clave: " + string.Join(",", actual_clave));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void UpdateText()
    {
        num = Random.Range(1, 99999);
        text_ran_gen_num.GetComponent<Text>().text = "" + num;
        // return seed;
    }

    private List<string> ClaveGenerator(string time_signature)
    {
        int binNum;
        string subdivision;
        List<string> all_possible_claves = new List<string>();

        if (time_signature == "3/4")
        {
            if (sub_division == "1/8")
            {
                all_possible_claves = new List<string>() { "33", "222" };
            }
            else //sub_division == "1/16"
            {
                List<string> perr34 = FindPermutations("22233");
                var per34 = RemoveDuplicatesSet(perr34);
                all_possible_claves = per34.Union<string>(perr34).ToList<string>();
                all_possible_claves.Add("222222");
                all_possible_claves.Add("3333");
            }

        }
        else if (time_signature == "4/4")
        {
            binNum = Random.Range(0, 1);
            if (sub_division == "1/8")
            {
                // clave para 4/4 con 1/8 de subdivision
                List<string> perrr = FindPermutations("332");
                all_possible_claves = RemoveDuplicatesSet(perrr);
                all_possible_claves.Add("2222");
            }
            else //sub_division == "1/16"
            {
                // clave para 4/4 con 1/16 de subdivision
                List<string> perr = FindPermutations("223333");
                List<string> perr2 = FindPermutations("2222233");
                var per = RemoveDuplicatesSet(perr);
                var per2 = RemoveDuplicatesSet(perr2);
                all_possible_claves = per.Union<string>(per2).ToList<string>();
                all_possible_claves.Add("22222222");
            }
        }

        string chosen_clave = all_possible_claves[Random.Range(0, all_possible_claves.Count())];
        List<string> chosen_clave_list = new List<string>();

        foreach (char s in chosen_clave){
            chosen_clave_list.Add(s.ToString());
        }
        return chosen_clave_list;
    }

    private int[] fill_generator(string time_signature)
    {
        int[] filler = new int[] { 1, 1, 1, 1, 1, 1 };
        return filler;
    }

    private static List<string> FindPermutations(string set)
    {
        var output = new List<string>();
        if (set.Length == 1)
        {
            output.Add(set);
        }
        else
        {
            foreach (var c in set)
            {
                // Remove one occurrence of the char (not all)
                var tail = set.Remove(set.IndexOf(c), 1);
                foreach (var tailPerms in FindPermutations(tail))
                {
                    output.Add(c + tailPerms);
                }
            }
        }
        return output;
    }

    public static List<T> RemoveDuplicatesSet<T>(List<T> items)
    {
        // Use HashSet to remember items seen.
        var result = new List<T>();
        var set = new HashSet<T>();
        for (int i = 0; i < items.Count; i++)
        {
            // Add if needed.
            if (!set.Contains(items[i]))
            {
                result.Add(items[i]);
                set.Add(items[i]);
            }
        }
        return result;
    }

    void ExampleRemoveDuplicates()
    {
        var input = new List<string>() { "j", "x", "j", "x", "y" };
        var output = RemoveDuplicatesSet(input);
        Debug.Log("Input: " + string.Join(",", input));
        Debug.Log("Output: " + string.Join(",", output));
    }

    void DebugMethod()
    {
        Debug.Log("clave para 3/4 con 1/8 de subdivision");
        List<string> er = new List<string>() { "33", "222" };
        Debug.Log("Output3: " + string.Join(",", er));
        Debug.Log("clave para 3/4 con 1/16 de subdivision");
        List<string> perr34 = FindPermutations("22233");
        var per34 = RemoveDuplicatesSet(perr34);
        List<string> all_claves32 = per34.Union<string>(perr34).ToList<string>();
        all_claves32.Add("222222");
        all_claves32.Add("3333");
        Debug.Log("Output3: " + string.Join(",", all_claves32));
        Debug.Log("clave para 4/4 con 1/8 de subdivision");
        List<string> perrr = FindPermutations("332");
        var all_claves = RemoveDuplicatesSet(perrr);
        all_claves.Add("2222");
        Debug.Log("Output: " + string.Join(",", all_claves));
        Debug.Log("clave para 4/4 con 1/16 de subdivision");
        List<string> perr = FindPermutations("223333");
        List<string> perr2 = FindPermutations("2222233");
        var per = RemoveDuplicatesSet(perr);
        var per2 = RemoveDuplicatesSet(perr2);
        List<string> all_claves2 = per.Union<string>(per2).ToList<string>();
        all_claves.Add("22222222");
        //Debug.Log("Output: " + string.Join(",", per));
        //Debug.Log("Output2: " + string.Join(",", per2));
        Debug.Log("Output3: " + string.Join(",", all_claves2));
    }

}

