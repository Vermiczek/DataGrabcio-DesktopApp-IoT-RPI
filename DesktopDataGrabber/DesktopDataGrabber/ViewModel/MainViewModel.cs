#define CLIENT
#define GET
#define DYNAMIC

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Timers;
using System.Net.Http;
using OxyPlot;
using OxyPlot.Axes;
using OxyPlot.Series;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;


namespace DesktopDataGrabber.ViewModel
{
    using Model;
    using System.Windows.Controls;
    using System.Windows.Media;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /** 
      * @brief View model for MainWindow.xaml 
      */
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties
        private string ipAddress;

        public ObservableCollection<TablesViewModel> Data_tables { get; set; }

        public string IpAddress
        {
            get
            {
                return ipAddress;
            }
            set
            {
                if (ipAddress != value)
                {
                    ipAddress = value;
                    OnPropertyChanged("IpAddress");
                }
            }
        }
        private int sampleTime;
        public string SampleTime
        {
            get
            {
                return sampleTime.ToString();
            }
            set
            {
                if (Int32.TryParse(value, out int st))
                {
                    if (sampleTime != st)
                    {
                        sampleTime = st;
                        OnPropertyChanged("SampleTime");
                    }
                }
            }
        }

        public PlotModel Chart_temp { get; set; }

        public PlotModel Chart_press { get; set; }

        public PlotModel Chart_humid { get; set; }
         
        public PlotModel ChartRPY { get; set; }

        public PlotModel ChartJOY { get; set; }
        public ButtonCommand StartButton { get; set; }

        public ButtonCommand SendLed { get; set; }

        public ButtonCommand ClearLed { get; set; }
        public ButtonCommand StopButton { get; set; }
        public ButtonCommand UpdateConfigButton { get; set; }
        public ButtonCommand DefaultConfigButton { get; set; }

        public Brush Gradient { get; set; }

        #endregion

        #region Fields
        private int timeStamp = 0;
        private ConfigParams config = new ConfigParams();
        private Timer RequestTimer;
        private IoTServer Server;
        #endregion

        public MainViewModel()
        {
            Data_tables = new ObservableCollection<TablesViewModel>();

            Chart_temp = new PlotModel { Title = "Temperature" };
            Chart_press = new PlotModel { Title = "Pressure" };
            Chart_humid = new PlotModel { Title = "Humidity" };
            ChartRPY = new PlotModel { Title = "Humidity" };
            ChartJOY = new PlotModel { Title = "Humidity" };


            Chart_temp.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            Chart_temp.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 30,
                Maximum = -20,
                Key = "Vertical",
                Unit = "-",
                Title = "Random value"
            });
            Chart_press.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            Chart_press.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 1200,
                Key = "Vertical",
                Unit = "-",
                Title = "Random value"
            });
            Chart_humid.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            Chart_humid.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 100,
                Key = "Vertical",
                Unit = "-",
                Title = "Random value"
            });

            ChartRPY.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            ChartRPY.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 100,
                Key = "Vertical",
                Unit = "-",
                Title = "Random value"
            });

            ChartJOY.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = -30,
                Maximum = 30,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            ChartJOY.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = -30,
                Maximum = 30,
                Key = "Vertical",
                Unit = "-",
                Title = "Random value"
            });

            Chart_humid.Series.Add(new LineSeries() { Title = "random data series", Color = OxyColor.Parse("#FFFF0000") });
            Chart_press.Series.Add(new LineSeries() { Title = "random data series", Color = OxyColor.Parse("#FFFF0000") });
            Chart_temp.Series.Add(new LineSeries() { Title = "random data series", Color = OxyColor.Parse("#FFFF0000") });

            ChartRPY.Series.Add(new LineSeries() { Title = "Roll", Color = OxyColor.Parse("#FFFF0000") });
            ChartRPY.Series.Add(new LineSeries() { Title = "Pitch", Color = OxyColor.Parse("#0000FF") });
            ChartRPY.Series.Add(new LineSeries() { Title = "Yaw", Color = OxyColor.Parse("#ffff00") });
            ChartJOY.Series.Add(new LineSeries() { Title = "Point placement", Color = OxyColor.Parse("#FFFF0000"),
                MarkerFill = OxyColors.Blue,
                MarkerStroke = OxyColors.Red,
                MarkerType = MarkerType.Circle,
                StrokeThickness = 0,
                MarkerSize = 4,
            });

            StartButton = new ButtonCommand(StartTimer);
            StopButton = new ButtonCommand(StopTimer);
            UpdateConfigButton = new ButtonCommand(UpdateConfig);
            DefaultConfigButton = new ButtonCommand(DefaultConfig);
            SendLed = new ButtonCommand(SendLedData);
            ClearLed = new ButtonCommand(ClearLedData);

            ipAddress = config.IpAddress;
            sampleTime = config.SampleTime;

            Server = new IoTServer(IpAddress);
        }

        /**
          * @brief Time series plot update procedure.
          * @param t X axis data: Time stamp [ms].
          * @param d Y axis data: Real-time measurement [-].
          */
        private void UpdatePlotT(double t, double d)
        {
            LineSeries lineSeries = Chart_temp.Series[0] as LineSeries;

            lineSeries.Points.Add(new DataPoint(t, d));

            if (lineSeries.Points.Count > config.MaxSampleNumber)
                lineSeries.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                Chart_temp.Axes[0].Minimum = (t - config.XAxisMax);
                Chart_temp.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }

            Chart_temp.InvalidatePlot(true);
        }

        private void UpdatePlotH(double t, double d)
        {
            LineSeries lineSeries = Chart_humid.Series[0] as LineSeries;

            lineSeries.Points.Add(new DataPoint(t, d));

            if (lineSeries.Points.Count > config.MaxSampleNumber)
                lineSeries.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                Chart_humid.Axes[0].Minimum = (t - config.XAxisMax);
                Chart_humid.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }

            Chart_humid.InvalidatePlot(true);
        }

        private void UpdatePlotP(double t, double d)
        {
            LineSeries lineSeries = Chart_press.Series[0] as LineSeries;

            lineSeries.Points.Add(new DataPoint(t, d));

            if (lineSeries.Points.Count > config.MaxSampleNumber)
                lineSeries.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                Chart_press.Axes[0].Minimum = (t - config.XAxisMax);
                Chart_press.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }

            Chart_press.InvalidatePlot(true);
        }



        private void UpdatePlotJoy(double t, double d)
        {
            LineSeries lineSeries = ChartJOY.Series[0] as LineSeries;
            lineSeries.Points.Add(new DataPoint(t, d));
            if (lineSeries.Points.Count>1)
            lineSeries.Points.RemoveAt(0);
         

            ChartJOY.InvalidatePlot(true);
        }

        private void UpdatePlotRPY(double t, double r, double p, double y)
        {
            LineSeries lineSeriesR = ChartRPY.Series[0] as LineSeries;
            LineSeries lineSeriesP = ChartRPY.Series[1] as LineSeries;
            LineSeries lineSeriesY = ChartRPY.Series[2] as LineSeries;

            lineSeriesR.Points.Add(new DataPoint(t, r)); 
            lineSeriesP.Points.Add(new DataPoint(t, p));
            lineSeriesY.Points.Add(new DataPoint(t, y)); 


            if (lineSeriesR.Points.Count > config.MaxSampleNumber)
                lineSeriesR.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                ChartRPY.Axes[0].Minimum = (t - config.XAxisMax);
                ChartRPY.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }



            ChartRPY.InvalidatePlot(true);
        }

        private async void SendLedData()
        {
            await Server.ClientSendLed(X, Y, R, G, B);
        }

        private async void ClearLedData()
        {
            
           await Server.ClientClearLed();
         
        }

        private string x = "0";
        public string X
        {
            get
            {
                return x;
            }
            set
            {
                if (x != value)
                {
                    x = value;
                    OnPropertyChanged("X");
                }
            }
        }
        private string y = "0";
        public string Y
        {
            get
            {
                return y;
            }
            set
            {
                if (y != value)
                {
                    y = value;
                    OnPropertyChanged("Y");
                    
                }
            }
        }

        private Brush Set_color(string R, string G, string B)
        {

            double red, green, blue;

            red = double.Parse(R);
            green = double.Parse(G);
            blue = double.Parse(B);

            Byte Rbyte = Convert.ToByte(red);
            Byte Gbyte = Convert.ToByte(green);
            Byte Bbyte = Convert.ToByte(blue);
            Color color_temp = Color.FromArgb(255, Rbyte, Gbyte, Bbyte);

            Brush brush = new SolidColorBrush(color_temp);
          
            return brush;

        }
        private string r = "0";
        public string R
        {
            get
            {
                return r;
            }
            set
            {
                if (r != value)
                {
                    r = value;
                    OnPropertyChanged("R");
                    Brush color;
                    color = Set_color(R, G, B);
                    Gradient = color;
                }
            }
        }
        private string g = "0";
        public string G
        {
            get
            {
                return g;
            }
            set
            {
                if (g != value)
                {
                    g = value;
                    OnPropertyChanged("G");
                    Brush color;
                    color = Set_color(R, G, B);
                    Gradient = color;
                }
            }
        }
        private string b = "0";
        public string B
        {
            get
            {
                return b;
            }
            set
            {
                if (b != value)
                {
                    b = value;
                    OnPropertyChanged("B");
                    Brush color;
                    color = Set_color(R, G, B);
                    Gradient = color;
                }
            }
        }
        /**
          * @brief Asynchronous chart update procedure with
          *        data obtained from IoT server responses.
          * @param ip IoT server IP address.
          */
        private async void UpdatePlotWithServerResponse()
        {
#if CLIENT
#if GET
            string responseText1 = await Server.GETwithClient();
            string responseText2 = await Server.GETwithClient_Array();
#else
            string responseText = await Server.POSTwithClient();
#endif
#else
#if GET
            string responseText = await Server.GETwithRequest();
#else
            string responseText = await Server.POSTwithRequest();
#endif
#endif

            try
            {

                App.Current.Dispatcher.Invoke((System.Action)delegate
                {
                    if (responseText2 != null)
                    {
                        JArray DataJsonArray = JArray.Parse(responseText2);

                        var Data_Tables_List = DataJsonArray.ToObject<List<TablesModel>>();

                        if (Data_tables.Count < Data_Tables_List.Count)
                        {
                            foreach (var d in Data_Tables_List)
                            {
                                Data_tables.Add(new TablesViewModel(d));
                            }
                        }
                    }
                });



#if DYNAMIC
                dynamic responseJson = JObject.Parse(responseText1);
                UpdatePlotT(timeStamp / 1000.0, (double)responseJson.Temperature);
                UpdatePlotH(timeStamp / 1000.0, (double)responseJson.Humidity);
                UpdatePlotP(timeStamp / 1000.0, (double)responseJson.Pressure);
                UpdatePlotRPY(timeStamp / 1000.0, (double)responseJson.Roll, (double)responseJson.Pitch, (double)responseJson.Yaw);
                UpdatePlotJoy((int)responseJson.x, (int)responseJson.y);
#else
                ServerData resposneJson = JsonConvert.DeserializeObject<ServerData>(responseText1);
                UpdatePlot(timeStamp / 1000.0, resposneJson.data);
#endif
            }
            catch (Exception e)
            {
                Debug.WriteLine("JSON DATA ERROR");
                Debug.WriteLine(responseText1);
                Debug.WriteLine(e);
            }

            timeStamp += config.SampleTime;
        }

        /**
          * @brief Synchronous procedure for request queries to the IoT server.
          * @param sender Source of the event: RequestTimer.
          * @param e An System.Timers.ElapsedEventArgs object that contains the event data.
          */
        private void RequestTimerElapsed(object sender, ElapsedEventArgs e)
        {
            UpdatePlotWithServerResponse();
        }

        #region ButtonCommands

        /**
         * @brief RequestTimer start procedure.
         */
        private void StartTimer()
        {
            if (RequestTimer == null)
            {
                RequestTimer = new Timer(config.SampleTime);
                RequestTimer.Elapsed += new ElapsedEventHandler(RequestTimerElapsed);
                RequestTimer.Enabled = true;

                Chart_humid.ResetAllAxes();
                Chart_press.ResetAllAxes();
                Chart_temp.ResetAllAxes();
            }
        }

        /**
         * @brief RequestTimer stop procedure.
         */
        private void StopTimer()
        {
            if (RequestTimer != null)
            {
                RequestTimer.Enabled = false;
                RequestTimer = null;
            }
        }

        /**
         * @brief Configuration parameters update
         */
        private void UpdateConfig()
        {
            bool restartTimer = (RequestTimer != null);

            if (restartTimer)
                StopTimer();

            config = new ConfigParams(ipAddress, sampleTime);
            Server = new IoTServer(IpAddress);

            if (restartTimer)
                StartTimer();
        }

        /**
          * @brief Configuration parameters defualt values
          */
        private void DefaultConfig()
        {
            bool restartTimer = (RequestTimer != null);

            if (restartTimer)
                StopTimer();

            config = new ConfigParams();
            IpAddress = config.IpAddress;
            SampleTime = config.SampleTime.ToString();
            Server = new IoTServer(IpAddress);

            if (restartTimer)
                StartTimer();
        }

        #endregion

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
