using RetailManagerUI.ViewModels.Common.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace RetailManagerUI.Views.Common.UI
{
    /// <summary>
    /// Interaction logic for MsgBoxV.xaml
    /// </summary>
    public partial class MsgBoxV : Window
    {
        public MsgBoxV()
        {
            InitializeComponent();
        }

        public MsgBoxV(MsgBoxVM param)
        {
            InitializeComponent();
            DataContext = param;
        }
    }
}
