using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class Screen : MonoBehaviour
    {
        private Text _text;
        void Start()
        {
            _text = GetComponent<Text>();
        }
        public void SetText(string text)
        {
            _text.text = text;
        }
        public void ClearText()
        {
            _text.text = "";
        }
    }
}