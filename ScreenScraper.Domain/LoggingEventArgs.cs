using System;

namespace ScreenScraper.Domain
{
    public class LoggingEventArgs : EventArgs
    {
        public string Message { get; set; }
    }
}