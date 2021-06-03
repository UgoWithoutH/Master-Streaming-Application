using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Master_Streaming
{
    /// <summary>
    /// Logique d'interaction pour UC_serie.xaml
    /// </summary>
    public partial class UC_serie : UserControl
    {
        public UC_serie()
        {
            InitializeComponent();
        }



        public string TextTitre
        {
            get { return (string)GetValue(TextTitreProperty); }
            set { SetValue(TextTitreProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Titre.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty TextTitreProperty =
            DependencyProperty.Register("TextTitre", typeof(string), typeof(UC_serie), new PropertyMetadata());



        public string ImageSource
        {
            get { return (string)GetValue(ImageSourceProperty); }
            set { SetValue(ImageSourceProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ImageSource.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty ImageSourceProperty =
            DependencyProperty.Register("ImageSource", typeof(string), typeof(UC_serie), new PropertyMetadata());


    }
}
