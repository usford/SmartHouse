using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net;
using System.Net.Sockets;
using UnityEngine.Networking;

public static class JsonHelper
{
    public static T[] FromJson<T>(string json)
    {
        Wrapper<T> wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
        return wrapper.Items;
    }

    public static string ToJson<T>(T[] array)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper);
    }

    public static string ToJson<T>(T[] array, bool prettyPrint)
    {
        Wrapper<T> wrapper = new Wrapper<T>();
        wrapper.Items = array;
        return JsonUtility.ToJson(wrapper, prettyPrint);
    }

    [System.Serializable]
    private class Wrapper<T>
    {
        public T[] Items;
    }
}

[System.Serializable]
public class myJson
{
    public int userId;
    public int id;
}

[System.Serializable]
public class myJsonList
{
    public List<myJson> myJson = new List<myJson>();
}

public class test : MonoBehaviour
{

    public myJsonList myJsonesList = new myJsonList();

    private void Start()
    {
        StartCoroutine(GetRequest("https://jsonplaceholder.typicode.com/posts"));
        //StartCoroutine(PostRequest("http:///www.yoururl.com", "your json"));
    }

    private void Awake()
    {
        
    }

    string fixJson(string value)
    {
        value = "{\"myJson\":" + value + "}";
        return value;
    }

    IEnumerator GetRequest(string uri)
    {
        
        UnityWebRequest uwr = UnityWebRequest.Get(uri);
        yield return uwr.SendWebRequest();
        string jsonString = fixJson(uwr.downloadHandler.text);
        //myJsonesList = JsonUtility.FromJson<myJsonList>(jsonString);
        //foreach(myJson mj in myJsonesList.myJson)
        //{
        //    print(mj.userId);
        //    print(mj.id);
        //    print(mj.title);
        //    print(mj.body);
        //}


        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            //Debug.Log("Получен: " + uwr.downloadHandler.text);
        }
    }

    IEnumerator PostRequest(string url, string json)
    {
        var uwr = new UnityWebRequest(url, "POST");
        byte[] jsonToSend = new System.Text.UTF8Encoding().GetBytes(json);
        uwr.uploadHandler = new UploadHandlerRaw(jsonToSend);
        uwr.downloadHandler = new DownloadHandlerBuffer();
        uwr.SetRequestHeader("Content-Type", "application/json");

        //Send the request then wait here until it returns
        yield return uwr.SendWebRequest();

        if (uwr.isNetworkError)
        {
            Debug.Log("Error While Sending: " + uwr.error);
        }
        else
        {
            Debug.Log("Received: " + uwr.downloadHandler.text);
           
        }
    }
}
