using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QJ1
{
    public class HealthSlider : MonoBehaviour
    {
        private Slider slider;
        float initialAlpha;
        [SerializeField] private Gradient sliderGradient;
        [SerializeField] private Image mainBarSprite;
        [SerializeField] private Image backBarSprite;

        private void Awake()
        {
            slider = GetComponent<Slider>();
            initialAlpha = backBarSprite.color.a;
        }

        // Start is called before the first frame update
        void Start()
        {
            Player.Instance.Damaged += Instance_Damaged;
            slider.maxValue = Player.Instance.MaxHealth;
            slider.value = Player.Instance.Health;
        }

        private void Instance_Damaged(object sender, System.EventArgs e)
        {
            slider.value = Player.Instance.Health;
            float t = slider.value / slider.maxValue;
            Color c1 = sliderGradient.Evaluate(t);
            Color c2 = c1;
            c2.a = initialAlpha;

            mainBarSprite.color = c1;
            backBarSprite.color = c2;
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}

