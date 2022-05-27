using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Views;
using UnityEngine.UI;

namespace Utils
{
    public class InputManager : MonoBehaviour
    {
        [SerializeField] private ButtonBase[] buttons;
        public Subject<string> OnButtonClicked = new Subject<string>();
        void Start()
        {
            SubscribeButtons();
        }

        private void SubscribeButtons()
        {
            foreach (ButtonBase button in buttons)
            {
                button.OnClickAsObservable()
                    .Subscribe(_ => {
                        OnButtonClicked.OnNext(button.ButtonId);
                    }).AddTo(this);
            }
        }
    }
}

