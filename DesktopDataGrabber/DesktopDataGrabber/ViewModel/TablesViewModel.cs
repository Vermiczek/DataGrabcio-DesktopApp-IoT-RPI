using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopDataGrabber.ViewModel
{
    using DesktopDataGrabber.Model;


    public class TablesViewModel : INotifyPropertyChanged
    {
        private string name_data;

        public string Name
        {
            get
            {
                return name_data;
            }
            set
            {
                if (name_data != value)
                {
                    name_data = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private double value_data;
        public string Value
        {
            get
            {
                return value_data.ToString("N1", CultureInfo.InvariantCulture);
            }
            set
            {
                if (Double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double tmp) && value_data != tmp)
                {
                    value_data = tmp;
                    OnPropertyChanged("Value");
                }
            }
        }

        private string unit_data;
        public string Unit
        {
            get
            {
                return unit_data;
            }
            set
            {
                if (unit_data != value)
                {
                    unit_data = value;
                    OnPropertyChanged("Unit");
                }
            }
        }


        public TablesViewModel(TablesModel model)
        {
            name_data = model.Name;
            OnPropertyChanged("Name");

            value_data = model.Value;
            OnPropertyChanged("Value");

            unit_data = model.Unit;
            OnPropertyChanged("Unit");

        }


        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        /**
         * @brief Simple function to trigger event handler
         * @params propertyName Name of ViewModel property as string
         */
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        #endregion
    }
}
