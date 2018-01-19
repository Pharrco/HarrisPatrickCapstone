using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSeqGen : MonoBehaviour {

    int value = 1;
    string output;

	// Use this for initialization
	void Start () {

        System.Random random = new System.Random();
        output = value.ToString() + ", ";

        for (int i = 0; i < 100; i++)
        {
            int rand = 0;

            do
            {
                rand = random.Next(-1, 2);
            }
            while ((value + rand > 5) || (value + rand < 1) || (rand == 0));

            value = value + rand;

            output += value.ToString() + ", ";
        }

	}
}
