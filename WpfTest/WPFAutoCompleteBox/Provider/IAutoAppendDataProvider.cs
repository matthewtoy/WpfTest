﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WPFAutoCompleteBox.Provider
{
    public interface IAutoAppendDataProvider
    {
        string GetAppendText(string textPattern, string firstMatch);
    }
}
