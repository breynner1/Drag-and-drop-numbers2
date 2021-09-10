using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Game
{
    public string idGame;
    public string nameGame;
    public string topic;
    public string score;
    public List<Level> levels;
}