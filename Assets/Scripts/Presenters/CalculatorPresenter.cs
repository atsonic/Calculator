using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils;
using Views;
using Models;
using UniRx;

namespace Presenters
{
    public class CalculatorPresenter : MonoBehaviour
    {
        [SerializeField] private InputManager _inputManager;
        [SerializeField] private CalculatorModel _model;
        [SerializeField] private Views.Screen _screen;

        void Start()
        {
            Init();
        }

        private void Init(){
            _inputManager.OnButtonClicked.Subscribe(buttonId => {
                _model.keyIsDown(buttonId);
            }).AddTo(this);

            _model.DisplayNum.SkipLatestValueOnSubscribe()
            .Subscribe(num => {
                _screen.SetText(num.ToString());
            }).AddTo(this);
        }
    }
}