using System;
using System.Collections.Generic;
using System.Text;

namespace VisionModels.Messages
{
    public class ObjectDetect
    {
        public string ImageId { get; set; }
        public string ObjectDescription { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
