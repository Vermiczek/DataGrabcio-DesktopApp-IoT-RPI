using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DesktopDataGrabcio.ViewModel
{
    using DesktopDataGrabcio.Model;

    //contains all the information neccessary to create a complete and working ListView using information aquired from the server. Uses TableModel.cs
    public class TablesViewModel : INotifyPropertyChanged
    {
        private string MeasurementName;

        public string Name
        {
            get
            {
                return MeasurementName;
            }
            set
            {
                if (MeasurementName != value)
                {
                    MeasurementName = value;
                    OnPropertyChanged("Name");
                }
            }
        }

        private double MeasurementValue;
        public string Value
        {
            get
            {
                return MeasurementValue.ToString("N1", CultureInfo.InvariantCulture);
            }
            set
            {
                if (Double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double tmp) && MeasurementValue != tmp)
                {
                    MeasurementValue = Convert.ToDouble(value);
                    OnPropertyChanged("Value");
                }
            }
        }

        private string MeasurementUnit;
        public string Unit
        {
            get
            {
                return MeasurementUnit;
            }
            set
            {
                if (MeasurementUnit != value)
                {
                    MeasurementUnit = value;
                    OnPropertyChanged("Unit");
                }
            }
        }


       

        public TablesViewModel(TableModel model)
        {
            UpdateWithModel(model);
        }

        public void UpdateWithModel(TableModel model)
        {
            MeasurementName = model.Name;
            OnPropertyChanged("Name");
            MeasurementValue = model.Value;
            OnPropertyChanged("Value");
            MeasurementUnit = model.Unit;
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
