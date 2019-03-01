using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace QJ1 {
    public class Timer : MonoBehaviour
    {
        private float elapsedTime = 0;
        private int iElapsedTime = -1;

        [SerializeField] private Text label;

        private void Start()
        {
            
        }

        private void Update()
        {
            if (!Player.Instance.IsAlive) { return; }

            elapsedTime += Time.deltaTime;
            if(iElapsedTime != (int)elapsedTime)
            {
                iElapsedTime = (int)elapsedTime;
                label.text = iElapsedTime.ToString();
            }
        }
    }
}


