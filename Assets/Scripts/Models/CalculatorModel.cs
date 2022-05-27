using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using Utils;

namespace Models
{
    public class CalculatorModel : MonoBehaviour
    {
        public ReactiveProperty<float> DisplayNum = new ReactiveProperty<float>();//Viewに渡す用の数値
        private float _tempNum = 0;//プラスマイナス符号を初期化
        private float _totalNum = 0;//計算結果を保存する変数
        private string _tempSymbol = "";//記号の一次保存用変数
        private string _calcSymbol = "";//演算用記号の一次保存用変数
        private bool _isPrevKeyIsNum = false;//ひとつ前のキーが数字かどうか
        private int _plusMinus = 1;//プラスマイナス判定符号
        public void keyIsDown(string key)
        {
            float i;
            if (float.TryParse(key, out i))//押されたキーが数値の場合
            {
                if(!_isPrevKeyIsNum)//一個前のキーが数値でない場合
                {
                    _tempNum = 0;//一次保存用の数値を初期化
                }
                _tempNum = Calculator.ChangeDigit(_tempNum, _plusMinus * i);// 数値が押されるたびに桁を増やす
                DisplayNum.Value = _tempNum;//キーが押されたタイミングで表示する数値を更新
                _isPrevKeyIsNum = true;//押されたキーが数値であることを示す
            }
            else//文字列の場合
            {
                switch (key){
                    case "=":// イコールの場合
                        if(!_isPrevKeyIsNum && _tempSymbol != "=")// 一個前が記号でかつイコールじゃ無い時は
                        {
                            _tempNum = _totalNum;//一次保存の数値を合計の数値にする
                        }
                        _totalNum = Calculator.Calculate(_totalNum, _tempNum, _calcSymbol);//計算結果を合計に記録
                        DisplayNum.Value = _totalNum;//表示用の数値を更新
                        break;
                    case ".":// ドットの場合
                        
                        break;
                    case "-":// マイナスの場合
                        if(_isPrevKeyIsNum)//一個前が数値の場合
                        {
                            if(_calcSymbol != "")//計算記号が保存されている場合は計算結果を合計に記録（連続計算用）
                            {
                                _totalNum = Calculator.Calculate(_totalNum, _tempNum, _calcSymbol);//計算結果を合計に記録
                            }
                            else//計算記号が保存されていない場合は
                            {
                                _totalNum = _tempNum;//一次保存の数値を合計に記録
                                _plusMinus = 1;//プラスマイナス符号を初期化
                            }
                            _calcSymbol = key;//演算記号を保存
                            _tempNum = 0;//一次保存の数値を初期化
                        }
                        else//一個前が数値じゃない場合
                        {
                            if(_tempNum == 0)//かつ一次保存用の数値が0の時（新たに数値を入れるタイミング）
                            {
                                _plusMinus = -1;//プラスマイナス符号をマイナスに（入力する数値をマイナスにする）
                            }
                        }
                        DisplayNum.Value = _totalNum;//表示用の数値を更新
                        break;
                    case "c":// クリアボタン（全初期化）
                        _plusMinus = 1;
                        _calcSymbol = "";
                        _totalNum = 0;
                        _tempNum = 0;
                        DisplayNum.Value = _totalNum;
                        break;
                    default:// 演算の記号の時
                        _plusMinus = 1;//プラスマイナス符号を初期化
                        if(_isPrevKeyIsNum)
                        {
                            if(_calcSymbol != "")
                            {
                                _totalNum = Calculator.Calculate(_totalNum, _tempNum, _calcSymbol);
                            }
                            else
                            {
                                _totalNum = _tempNum;
                            }
                        }
                        _calcSymbol = key; //演算記号を保存
                        DisplayNum.Value = _totalNum;//表示用の数値を更新
                        _tempNum = 0; //一次保存の数値を初期化
                        break;
                }
                _tempSymbol = key;//押された記号を一次保存
                _isPrevKeyIsNum = false;//押されたキーが記号であることを示す
            }
        }
    }
}