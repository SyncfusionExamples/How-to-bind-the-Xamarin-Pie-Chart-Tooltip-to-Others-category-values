﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SimpleSample
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PhotoLayout : ContentView
    {
        public PhotoLayout()
        {
            InitializeComponent();
        }
    }
}