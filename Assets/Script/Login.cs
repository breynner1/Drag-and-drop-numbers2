using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Bolt;

public class Login : MonoBehaviour
{
    [SerializeField] InputField username;
	[SerializeField] InputField password;
    [SerializeField] Button loginBtn;
    [SerializeField] Dropdown school;
    [SerializeField] Text errors;
    [SerializeField] string url;
    [SerializeField] string sceneName;
    [SerializeField] string gameName;

    private string name;

    WWWForm form;

    void Start()
    {
        StartCoroutine(GetColegiosList());
    }

    public void OnLoginBtnClicked()
	{
		Debug.Log("click!");
		loginBtn.interactable = false;
		StartCoroutine(Log());
	}

    IEnumerator GetColegiosList()
    {
        ColegioListObject colegiosList;
        string json;
        UnityWebRequest request = UnityWebRequest.Get("https://vip-epics-dev.herokuapp.com/getSchools");
        yield return request.SendWebRequest();
 
            if (request.isNetworkError) {
                errors.text = "Comprueba tu conexión a Internet";
                Debug.Log(request.error);
            }
            else {
                string content = System.Text.Encoding.UTF8.GetString(request.downloadHandler.data);
                string json_object = $" \"colegioList\" : {content}";
                json = "{\n"+json_object+"}";
                Debug.Log(json);
                colegiosList = JsonUtility.FromJson<ColegioListObject>(json);
                List<string> m_DropOptions = new List<string>();
                foreach (Colegio c in colegiosList.colegioList){
                    m_DropOptions.Add(c.Name);
                }
                school.AddOptions(m_DropOptions);
            }
    }

    IEnumerator Log(){
        form = new WWWForm ();
        int drop_option = school.value;
        string student_school =  school.options[drop_option].text;
        
        form.AddField("school", student_school);
        
		form.AddField ("username", username.text);
    
		form.AddField ("password", password.text);

        form.AddField ("game", gameName);
        
        

		UnityWebRequest www = UnityWebRequest.Post(url, form);
        yield return www.SendWebRequest();
     
        if (www.isNetworkError) { 
            errors.text = "Comprueba tu conexión a Internet";
			Debug.Log("<color=red>"+www.result+"</color>");
        }
        else {
            if (www.isDone) {
                string response = www.downloadHandler.text;
                Debug.Log("#1: "+ response);
                User user = JsonUtility.FromJson<User>(response);
                    if(user.username == "error"){
                        errors.text = "Usuario, contraseña o Colegio incorrecto/s";
                    } else{
                            Variables.Application.Set("username",user.username); // Coloca el nombre de usuario loggeado en la variable de aplicación username
                            Variables.Application.Set("colegio",student_school);
                            Variables.Application.Set("score",user.score);
                            Variables.Application.Set("game_name",gameName);
                            Variables.Application.Set("last_lvl",user.lastlevel);
                            Variables.Application.Set("win",user.win);
                            SceneManager.LoadScene(sceneName);
                    }
			}
        }
		loginBtn.interactable = true;
	}
}

