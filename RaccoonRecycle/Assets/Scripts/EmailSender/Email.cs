using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Email : MonoBehaviour
{
    [SerializeField]
    public TMP_InputField email;
    [SerializeField]
    private GameObject warning_SL;
    [SerializeField]
    private Text warningText; //warning �zenet dekral�l�sa
    [SerializeField]
    private string emailText;

    public void onSendClick()
    {
        StartCoroutine(tryEmailSend());
    }

    private IEnumerator tryEmailSend()
    {
        emailText = email.text;
        WWWForm form = new WWWForm(); //l�trehozunk egy body fel�p�t�st a k�r�s�nknek.
        form.AddField("email", emailText);//hozz�adjuk a bodyhoz az aUsername mez?t �s a username �rt�ket hozz� rendelj�k.
        var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/mail", form); // elk�ldj�k a webrequestet a megadott c�mre, bodyban a formmal.
        var handler = request.SendWebRequest();
        float startTime = 0f;

        while (!handler.isDone)
        {
            startTime += Time.deltaTime;
            if (startTime > 10.0f)
            {
                break;
            }
            yield return null;
        }
        if (request.result == UnityWebRequest.Result.Success)
        {

            warning_SL.SetActive(true);
            warningText.text = request.downloadHandler.text;
        }
        else
        {
            warning_SL.SetActive(true);
            warningText.text = "The game was unable to connect to the server!";
        }


        yield return null;
    }

}
