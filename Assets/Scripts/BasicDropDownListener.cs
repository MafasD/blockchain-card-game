using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BasicDropDownListener
{
    private Action<int> myMethod;

    public BasicDropDownListener(TMP_Dropdown dropdown, Action<int> myMethod)
    {
        this.myMethod = myMethod;

        dropdown.onValueChanged.AddListener(delegate
        {
            this.myMethod(dropdown.value);

        });
    }


}
