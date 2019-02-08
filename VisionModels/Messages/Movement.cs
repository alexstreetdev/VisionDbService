using System;
using System.Collections.Generic;

namespace VisionModels.Messages
{
    public class Movement : IMessage
    {
        public string ImageId { get; set; }
        public string CameraId { get; set; }

        public string CorrelationId { get; set; }
        public int SequenceNumber { get; set; }
        public DateTime EventTime { get; set; }
        public string Filename { get; set; }
        public string Url { get; set; }
        public List<Region> Regions { get; set; }
    }
}