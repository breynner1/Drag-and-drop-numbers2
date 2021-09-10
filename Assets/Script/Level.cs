using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string startTime;
    public string finishTime;
    public string level;
    public List<Parameter> parameters;
    public Level(string lvl, List<Parameter> prms){
        parameters = prms;
        level = lvl;
    }
}
