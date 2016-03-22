﻿/******************************************************************************* 
  * Copyright 2016 Esri 
  *  
  *  Licensed under the Apache License, Version 2.0 (the "License"); 
  *  you may not use this file except in compliance with the License. 
  *  You may obtain a copy of the License at 
  *  
  *  http://www.apache.org/licenses/LICENSE-2.0 
  *   
  *   Unless required by applicable law or agreed to in writing, software 
  *   distributed under the License is distributed on an "AS IS" BASIS, 
  *   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. 
  *   See the License for the specific language governing permissions and 
  *   limitations under the License. 
  ******************************************************************************/ 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ArcGIS.Desktop.Framework;
using ArcGIS.Desktop.Framework.Contracts;
using ArcGIS.Desktop.Framework.Controls;
using System.Xml.Linq;
using System.Windows.Data;
using System.Windows;

namespace ProAppCoordToolModule.UI
{
    /// <summary>
    /// ViewModel for the embeddable control.
    /// </summary>
    internal class FlashEmbeddedControlViewModel : EmbeddableControl
    {
        public FlashEmbeddedControlViewModel(XElement options)
            : base(options)
        {
        }

        private bool _flash = true;
        public bool Flash
        {
            get
            {
                return _flash;
            }
            set
            {
                SetProperty(ref _flash, value, () => Flash);
            }
        }
        /// <summary>
        /// Property used to display the coordinates of the click location
        /// </summary>
        private string _clickText = "Click in view to show coordinates";
        public string ClickText
        {
            get { return _clickText; }
            set
            {
                SetProperty(ref _clickText, value, () => ClickText);
            }
        }
        private System.Windows.Point _clientPoint = new System.Windows.Point(0, 0);
        public System.Windows.Point ClientPoint
        {
            get { return _clientPoint; }
            set
            {
                SetProperty(ref _clientPoint, value, () => ClientPoint);
            }
        }
        private System.Windows.Point _screenPoint = new System.Windows.Point(0, 0);
        public System.Windows.Point ScreenPoint
        {
            get { return _screenPoint; }
            set
            {
                SetProperty(ref _screenPoint, value, () => ScreenPoint);
                //Flash = false;
                Flash = true;
                Flash = false;
            }
        }
    }

    internal class ScreenToClientPointConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            var screenPoint = (System.Windows.Point)values[0];
            var c = values[1] as System.Windows.Controls.Canvas;
            if (c == null)
                return -999;

            var source = PresentationSource.FromVisual(c);

            if (source == null)
                return 0;

            var point = c.PointFromScreen(screenPoint);
            var ps = parameter.ToString();
            if (ps == "X")
                return point.X;
            else
                return point.Y;
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
