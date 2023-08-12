using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
using Unity.Collections;
using Unity.Networking.Transport;
using UnityEngine.Networking;
using System;


[Serializable]
public class Data
{
public string image;
}

public class picshower : MonoBehaviour
{
public string addr = "localhost";  // "localhost";  //"10.241.179.2";
public ushort port = 9000;
void Start() {
        InvokeRepeating("ChangePic", 1, 1);

}
void ChangePic() {
StartCoroutine(ChangeReal());
}
IEnumerator ChangeReal() {
    UnityWebRequest uwr = UnityWebRequest.Get("https://" + addr + ":"+ "/api");
    Debug.Log("waiting...");
yield return uwr.SendWebRequest();
Debug.Log("waited");

// Check if there is an error
if (uwr.isNetworkError || uwr.isHttpError)
{
// Log the error
Debug.Log("Error: " + uwr.error);
}
else
{
    Debug.Log("parsing");
// Get the response as a string
string json = uwr.downloadHandler.text;

// Parse the JSON string into a Data object
Data data = JsonUtility.FromJson<Data>(json);

// Log the data fields
byte[] img = Convert.FromBase64String (data.image);
Debug.Log("loaded");

var texture = new Texture2D(128, 128);
texture.LoadImage(img);
Debug.Log("texture");
GetComponent<MeshRenderer>().materials[0].mainTexture = texture;

Debug.Log("done");

}
}
void Update() {

}
}
