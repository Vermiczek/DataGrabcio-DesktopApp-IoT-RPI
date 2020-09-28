namespace DesktopDataGrabber.Model
{
    /**
     * @brief Simple parseable data model for IoT server response
     */ 
    public class ServerData
    {
        public double Temperature { get; set; }
        public double Pressure { get; set; }
        public double Humidity { get; set; }

        public double Roll { get; set; }

        public double Pitch { get; set; }

        public double Yaw { get; set; }

        public int x { get; set; }

        public int y { get; set; }
    }
}
