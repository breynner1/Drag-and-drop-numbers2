using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Parameter
{
    public string name;
    public string value;

    public Parameter(string nam, string val){
        name = nam;
        value = val;
    }
}
