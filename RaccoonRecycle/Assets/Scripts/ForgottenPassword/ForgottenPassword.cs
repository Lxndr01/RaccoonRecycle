using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForgottenPassword : MonoBehaviour
{
    [SerializeField]
    private TMP_InputField generatedCode;
    [SerializeField]
    private TMP_InputField newPassword;
    [SerializeField]
    private TMP_InputField newPassword2;
    [SerializeField]
    private GameObject warning_SL;
    [SerializeField]
    private Text warningText; //warning �zenet dekral�l�sa.

    public static string localUserName;

    public static string token;


    public void onSaveClick()
    {
        StartCoroutine(tryForgotPassword());
    }

    private IEnumerator tryForgotPassword()
    {
        if (newPassword.text != newPassword2.text)
        {
            warning_SL.SetActive(true);
            warningText.text = "The password do not match!";
        }
        else
        {
            WWWForm form = new WWWForm(); //l�trehozunk egy body fel�p�t�st a k�r�s�nknek.
            form.AddField("generatedCode", generatedCode.text);//hozz�adjuk a bodyhoz az aUsername mez?t �s a username �rt�ket hozz� rendelj�k.
            form.AddField("newPassword", newPassword.text); //hozz�adjuk a bodyhoz az aPassowrd mez?t �s a password �rt�ket hozz� rendelj�k.
            var request = UnityWebRequest.Post("http://188.166.166.197:18102/api/passwordchange", form); // elk�ldj�k a webrequestet a megadott c�mre, bodyban a formmal.
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
                token = request.downloadHandler.text;
                localUserName = request.downloadHandler.text;
                SceneManager.LoadScene(1);
            }
            else
            {
                warning_SL.SetActive(true);
                warningText.text = "The game was unable to connect to the server!";
            }
        }

        yield return null;
    }

}
