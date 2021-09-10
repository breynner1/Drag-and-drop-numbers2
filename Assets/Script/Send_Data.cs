using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using Bolt;
using System;

public class Send_Data : MonoBehaviour
{
   public SessionGame session;
   public string level;
   public int score_lvl;

   public void send(List<Parameter> prms){
       string username = (string)Variables.Application.Get("username");
       string school = (string)Variables.Application.Get("colegio");
       session.Student = new Student(username,school);
       session.Game.levels.Add(new Level(level,prms));
       session.Game.nameGame = (string)Variables.Application.Get("game_name");
       string global_score = (string)Variables.Application.Get("score");
       session.Game.score = (score_lvl + Int32.Parse(global_score)).ToString();
       Variables.Application.Set("score",session.Game.score);
       string json = JsonUtility.ToJson(session);
        UnityWebRequest webRequest = new UnityWebRequest("https://vip-epics-dev.herokuapp.com/sessionGame", "PUT");
        byte[] encodedPayload = new System.Text.UTF8Encoding().GetBytes(json);
        webRequest.uploadHandler = (UploadHandler) new UploadHandlerRaw(encodedPayload);
        webRequest.downloadHandler = (DownloadHandler) new DownloadHandlerBuffer();
        webRequest.SetRequestHeader("Content-Type", "application/json");
        
        UnityWebRequestAsyncOperation requestHandel = webRequest.SendWebRequest();
        requestHandel.completed += delegate(AsyncOperation pOperation) {
            Debug.Log(webRequest.responseCode);
            Debug.Log(webRequest.downloadHandler.text);
        };
   }

   public void setLevel(string lvl){
       this.level = lvl;
   }

   public Parameter parameter(string name,string val){
      return new Parameter(name,val);
  }
  public void levelScore(int score){
      score_lvl = score;
  }
}
