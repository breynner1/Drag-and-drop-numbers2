using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Student
{
  public string username;
  public string school;

  public Student(string usrname, string schoool){
    username = usrname;
    school = schoool;
  }
}
