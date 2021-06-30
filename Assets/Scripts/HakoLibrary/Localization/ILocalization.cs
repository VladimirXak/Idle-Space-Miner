using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace HakoLibrary.LocalizationSpace
{
    public interface ILocalization
    {
        event Action<ILocalization> OnChangeLanguage;

        string GetValue(string key);
    }
}
