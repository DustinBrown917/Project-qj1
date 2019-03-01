using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeAwayText : MonoBehaviour
{
    private Coroutine cr_FadeAway;
    private Text textToFade;
    [SerializeField] private float colorFadeRate;


    // Start is called before the first frame update
    void Start()
    {
        textToFade = GetComponent<Text>();
        CoroutineManager.BeginCoroutine(Delay(), ref cr_FadeAway, this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FadeAway()
    {
        while(textToFade.color.a > 0)
        {
            Color c = textToFade.color;
            c.a -= colorFadeRate * Time.deltaTime;
            textToFade.color = c;
            yield return null;
        }
    }

    private IEnumerator Delay()
    {
        yield return new WaitForSeconds(1.0f);
        CoroutineManager.BeginCoroutine(FadeAway(), ref cr_FadeAway, this);
    }
}
