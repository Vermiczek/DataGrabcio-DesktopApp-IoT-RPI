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


namespace DesktopDataGrabcio.ViewModel
{
    using Model;
    using System.Windows.Media;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;

    /** 
      * @brief View Model for MainWindow.xaml 
      */
    public class MainViewModel : INotifyPropertyChanged
    {
        #region Properties
        private string ipAddress;

        public ObservableCollection<TablesViewModel> DataTable { get; set; }

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

        private int maxSampleNumber;
        public string MaxSampleNumber
        {
            get
            {
                return maxSampleNumber.ToString();
            }
            set
            {
                if (Int32.TryParse(value, out int sn))
                {
                    if (sampleTime != sn)
                    {
                        maxSampleNumber = sn;
                        OnPropertyChanged("MaxSampleNumber");
                    }
                }
            }
        }

        public PlotModel ChartTemp { get; set; }

        public PlotModel ChartPress { get; set; }

        public PlotModel ChartHumid { get; set; }
         
        public PlotModel ChartRPY { get; set; }

        public PlotModel ChartJOY { get; set; }
        public ButtonCommand StartButton { get; set; }

        public ButtonCommand SendLed { get; set; }

        public ButtonCommand ClearLed { get; set; }
        public ButtonCommand StopButton { get; set; }
        public ButtonCommand UpdateConfigButton { get; set; }
        public ButtonCommand DefaultConfigButton { get; set; }

        public Brush Gradient { get; set; }

        int ResetTimer = 0;

        #endregion

        #region Fields
        private int timeStamp = 0;
        private ConfigParams config = new ConfigParams();
        private Timer RequestTimer;
        private IoTServer Server;
        #endregion

        //MainView model, contains declarations of charts and buttons, their attributes and assigned functions
        public MainViewModel()
        {
            DataTable = new ObservableCollection<TablesViewModel>();

            ChartTemp = new PlotModel { Title = "Temperature" };
            ChartPress = new PlotModel { Title = "Pressure" };
            ChartHumid = new PlotModel { Title = "Humidity" };
            ChartRPY = new PlotModel { Title = "Roll, Pitch, Yaw angles" };
            ChartJOY = new PlotModel { Title = "Joystick" };


            ChartTemp.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            ChartTemp.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 30,
                Maximum = -20,
                Key = "Vertical",
                Unit = "C",
                Title = "Temperature"
            });
            ChartPress.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            ChartPress.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 1200,
                Key = "Vertical",
                Unit = "mbar",
                Title = "Pressure"
            });
            ChartHumid.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "sec",
                Title = "Time"
            });
            ChartHumid.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 100,
                Key = "Vertical",
                Unit = "%",
                Title = "Humidity"
            });

            ChartRPY.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = 0,
                Maximum = config.XAxisMax,
                Key = "Horizontal",
                Unit = "s",
                Title = "Time"
            });
            ChartRPY.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = 0,
                Maximum = 360,
                Key = "Vertical",
                Unit = "Degrees",
                Title = "Angle value"
            });

            ChartJOY.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Bottom,
                Minimum = -30,
                Maximum = 30,
                Key = "Horizontal",
                Unit = "x",
            });
            ChartJOY.Axes.Add(new LinearAxis()
            {
                Position = AxisPosition.Left,
                Minimum = -30,
                Maximum = 30,
                Key = "Vertical",
                Unit = "y",
            });

            ChartHumid.Series.Add(new LineSeries() { Title = "Humidity", Color = OxyColor.Parse("#FFFF0000") });
            ChartPress.Series.Add(new LineSeries() { Title = "Pressure", Color = OxyColor.Parse("#FFFF0000") });
            ChartTemp.Series.Add(new LineSeries() { Title = "Temperature", Color = OxyColor.Parse("#FFFF0000") });

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
            maxSampleNumber = config.MaxSampleNumber;

            Server = new IoTServer(IpAddress);
        }

        
        //updates temperature chart
        private void UpdatePlotT(double t, double d)
        {
            LineSeries lineSeries = ChartTemp.Series[0] as LineSeries;

            lineSeries.Points.Add(new DataPoint(t, d));

            if (lineSeries.Points.Count > config.MaxSampleNumber)
                lineSeries.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                ChartTemp.Axes[0].Minimum = (t - config.XAxisMax);
                ChartTemp.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }

            ChartTemp.InvalidatePlot(true);
        }

        //updated humidity chart
        private void UpdatePlotH(double t, double d)
        {
            LineSeries lineSeries = ChartHumid.Series[0] as LineSeries;

            lineSeries.Points.Add(new DataPoint(t, d));

            if (lineSeries.Points.Count > config.MaxSampleNumber)
                lineSeries.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                ChartHumid.Axes[0].Minimum = (t - config.XAxisMax);
                ChartHumid.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }

            ChartHumid.InvalidatePlot(true);
        }

        //updates pressure chart
        private void UpdatePlotP(double t, double d)
        {
            LineSeries lineSeries = ChartPress.Series[0] as LineSeries;

            lineSeries.Points.Add(new DataPoint(t, d));

            if (lineSeries.Points.Count > config.MaxSampleNumber)
                lineSeries.Points.RemoveAt(0);

            if (t >= config.XAxisMax)
            {
                ChartPress.Axes[0].Minimum = (t - config.XAxisMax);
                ChartPress.Axes[0].Maximum = t + config.SampleTime / 1000.0; ;
            }

            ChartPress.InvalidatePlot(true);
        }


        //updates joystick chart, creates a single point with current joystick coordinates
        private void UpdatePlotJoy(double t, double d)
        {
            LineSeries lineSeries = ChartJOY.Series[0] as LineSeries;
            lineSeries.Points.Add(new DataPoint(t, d));
            if (lineSeries.Points.Count>1)
            lineSeries.Points.RemoveAt(0);
         

            ChartJOY.InvalidatePlot(true);
        }

        //updates angles chart, creates many lineseries on one chart
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

        //sends LED data to server script
        private async void SendLedData()
        {
            await Server.ClientSendLed(X, Y, R, G, B);
        }


        //creates and updates datatables
        private void makeDatatable(string responseText)
        {
            App.Current.Dispatcher.Invoke((System.Action)delegate
            {
                if (responseText != null)
                {
                    JArray DataJsonArray = JArray.Parse(responseText);
                    var DataTableList = DataJsonArray.ToObject<List<TableModel>>();
                    if (DataTable.Count < DataTableList.Count)
                    {
                        foreach (var d in DataTableList)
                        {
                            DataTable.Add(new TablesViewModel(d));
                        }
                    }
                    else
                    {
                        for (int i = 0; i < DataTable.Count; i++)
                            DataTable[i].UpdateWithModel(DataTableList[i]);
                    }
                }
            });


        }
        //Requests clearing of LED matrix
        private async void ClearLedData()
        {
            
           await Server.ClientClearLed();
         
        }

        //Declarations of coordinates used in led control to point at the exact LED we want to change

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

        //Declarations of RGB values used in LED matrix
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
                   
                }
            }
        }
        
     
        //Connects to network, updates charts and listView
        private async void UpdatePlotWithServerResponse()
        {  //acquires server response and assigns it to a string variable
            string responseText1 = await Server.GETwithClient();
            string responseText2 = await Server.GETwithClientTableview();

            //Managing datatable and charts data
            try
            {

                makeDatatable(responseText2);


                //Updates charts
                dynamic responseJson = JObject.Parse(responseText1);
                UpdatePlotT(timeStamp / 1000.0, (double)responseJson.Temperature);
                UpdatePlotH(timeStamp / 1000.0, (double)responseJson.Humidity);
                UpdatePlotP(timeStamp / 1000.0, (double)responseJson.Pressure);
                UpdatePlotRPY(timeStamp / 1000.0, (double)responseJson.Roll, (double)responseJson.Pitch, (double)responseJson.Yaw);
                UpdatePlotJoy((int)responseJson.x, (int)responseJson.y);

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

                ChartHumid.ResetAllAxes();
                ChartPress.ResetAllAxes();
                ChartTemp.ResetAllAxes();
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

            config = new ConfigParams(ipAddress, sampleTime, maxSampleNumber);
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
