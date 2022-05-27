using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Calculator : MonoBehaviour
{
    public static float ChangeDigit(float tempNum, float keyNum)
    {
        float num = 0;
        if (tempNum > 0 || tempNum < 0)
        {
            num = tempNum * 10 + keyNum;
        }
        else
        {
            num = keyNum;
        }
        return num;
    }
    public static float Calculate(float tempNum, float keyNum, string symbol)
    {
        float result;
        switch(symbol){
            case "+":
                result = tempNum + keyNum;
                break;
            case "-":
                result = tempNum - keyNum;
                break;
            case "*":
                result = tempNum * keyNum;
                break;
            case "/":
                result = tempNum / keyNum;
                break;
            default:
                result = 0;
                break;
        }
        return result;
    }
}
