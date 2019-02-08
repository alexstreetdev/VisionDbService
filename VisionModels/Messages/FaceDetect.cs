

namespace VisionModels.Messages
{
    public class FaceDetect : IMessage
    {
        public string ImageId { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
