using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingProgress : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TMP_Text loadingText;

    private void Start()
    {
        StartCoroutine(Progress());
    }

    IEnumerator Progress()
    {
        image.fillAmount = 0;
        yield return new WaitForSeconds(1);

        var asyncOp = SceneManager.LoadSceneAsync(SceneLoader.SceneToLoad);

        while (asyncOp.isDone == false)
        {
            image.fillAmount = asyncOp.progress;
            loadingText.text = "Loading " + (int)(asyncOp.progress * 100) + "%";
            yield return null;
        }
    }

}
