﻿using System.Windows.Input;

namespace WpfTest
{
    public class MyCommandWrapper
    {
        public ICommand Command { get; set; }
        public string DisplayName { get; set; }
    }
}