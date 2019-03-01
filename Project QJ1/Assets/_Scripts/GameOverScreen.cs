using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScreen : MonoBehaviour
{
    [SerializeField] private float fadeRate = 1.0f;
    private CanvasGroup cg;
    private Coroutine cr_FadeIn;

    private void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    private void OnEnable()
    {
        CoroutineManager.BeginCoroutine(WaitToFade(), ref cr_FadeIn, this);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator FadeIn()
    {
        while(cg.alpha < 1)
        {
            cg.alpha += fadeRate * Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator WaitToFade()
    {
        yield return new WaitForSeconds(3.0f);
        CoroutineManager.BeginCoroutine(FadeIn(), ref cr_FadeIn, this);
    }

    public void LoadScene(string s)
    {
        SceneManager.LoadScene(s);
    }
}
