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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace DesktopDataGrabcio.View
{
    /** 
     * @brief Interaction logic for MainWindow.xaml 
     */
    public partial class MainWindow : Window
    {
        bool isMenuVisible = true;

        public MainWindow()
        {
            InitializeComponent();
        }
       
        //Opens charts, hides anything else
        private void Button_THP(object sender, RoutedEventArgs e)
        {
            this.LedMatrix.Visibility = Visibility.Collapsed;
            this.DataPlotViewTemp.Visibility = Visibility.Visible;
            this.DataPlotViewPressure.Visibility = Visibility.Visible;
            this.DataPlotViewHumid.Visibility = Visibility.Visible;
            this.DataPlotViewRPY.Visibility = Visibility.Visible;
            this.DataPlotViewJOY.Visibility = Visibility.Visible;
            this.Data_table.Visibility = Visibility.Collapsed;

        }

        //Opens tableview, hides anything else
        private void Button_TV(object sender, RoutedEventArgs e)
        {
            this.DataPlotViewTemp.Visibility = Visibility.Collapsed;
            this.DataPlotViewPressure.Visibility = Visibility.Collapsed;
            this.DataPlotViewHumid.Visibility = Visibility.Collapsed;
            this.DataPlotViewRPY.Visibility = Visibility.Collapsed;
            this.DataPlotViewJOY.Visibility = Visibility.Collapsed;
            this.LedMatrix.Visibility = Visibility.Collapsed;
            this.Data_table.Visibility = Visibility.Visible;
        }



        //Opens LED, hides anything else
        private void Button_LED(object sender, RoutedEventArgs e)
        {
            this.DataPlotViewTemp.Visibility = Visibility.Collapsed;
            this.DataPlotViewPressure.Visibility = Visibility.Collapsed;
            this.DataPlotViewHumid.Visibility = Visibility.Collapsed;
            this.DataPlotViewRPY.Visibility = Visibility.Collapsed;
            this.DataPlotViewJOY.Visibility = Visibility.Collapsed;
            this.LedMatrix.Visibility = Visibility.Visible;
            this.Data_table.Visibility = Visibility.Collapsed;

        }
        private void MenuBtn_Click(object sender, RoutedEventArgs e)
        {
            isMenuVisible = !isMenuVisible;
           

            if (isMenuVisible)
                this.Menu.Visibility = Visibility.Visible;
            else
                this.Menu.Visibility = Visibility.Collapsed;
           
        }
    }
}
