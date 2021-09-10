using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Bolt;

public class Main : MonoBehaviour
{
    
    void Start()
    {
        StartCoroutine(GetLvl());
    }
    IEnumerator GetLvl()
    {
         string username = (string)Variables.Application.Get("username"); // Coloca el nombre de usuario loggeado en la variable de aplicación username
         string game_name = (string)Variables.Application.Get("game_name");
        string json;
        UnityWebRequest request = UnityWebRequest.Get($"https://vip-epics-dev.herokuapp.com/LastSessionStudentGame?game={game_name}&username={username}");
        yield return request.SendWebRequest();
 
            if (request.isNetworkError) {
                Debug.Log(request.error);
            }
            else {
                
                string response = request.downloadHandler.text;
                Debug.Log("respuesta del server: "+response);
                User user = JsonUtility.FromJson<User>(response);
                Variables.Application.Set("last_lvl",user.lastlevel);
                Variables.Application.Set("score",user.score);
                Variables.Application.Set("win",user.win);
                }
            }
    }

