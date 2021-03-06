﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SkiaSharp;
using SkiaSharp.Views.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace NeumorphicClock
{

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NeumorphicView : ShadowsElement
    {
        public NeumorphicView()
        {
            InitializeComponent();
            BindingContext = new NeuViewModel();

            Debug.WriteLine($"Neumorphism view BindingContext: {BindingContext}");
         
            PancakeView.BackgroundGradientStartPoint = new Point(0,0);
            PancakeView.BackgroundGradientEndPoint = new Point(1, 1);
            GradientOne.Color = Color.FromHex("ffffff");
            GradientTwo.Color = Color.FromHex("d8d8db");
        }

        public bool clicked { get; set; }
        private bool _isAnimate;
        private async void Animation()
        {
            //e7f3ea
            //c2ccc5
            var colorOne = Color.FromHex("ffffff");
            var colorTwo = Color.FromHex("d8d8db");
            var fade = 1.0;
            if(!clicked)
            await Task.WhenAll(
                this.ColorTo(colorOne, colorTwo, c => GradientOne.Color = c, 200),
                PancakeView.ColorTo(colorTwo, colorOne, c => GradientTwo.Color = c, 200));
            else
            {
                await Task.WhenAll(
                    this.ColorTo(colorTwo, colorOne, c => GradientOne.Color = c, 200),
                    PancakeView.ColorTo(colorOne,colorTwo , c => GradientTwo.Color = c, 200));
            }


            clicked = !clicked;
        }
        void OnCancelAnimationButtonClicked(object sender, EventArgs e)
        {
        }
        public override void OnIsCompactChanged()
        {
            if (IsCompact)
            {
                Description.Height = 0;
            }
        }
        private Animation _animation;
        private void ImageButtonOnClicked(object sender, EventArgs e)
        {
            Debug.WriteLine($"ButtonPlusNeuShadows BindingContext: {ButtonPlusNeuShadows.BindingContext}");
            foreach (var shade in ButtonPlusNeuShadows.Shades)
            {
                Debug.WriteLine($"ButtonPlusShadows shade BindingContext: {shade.BindingContext}");
            }

            Animation();

        }

    }



    public class NeuViewModel
    {
    }
}