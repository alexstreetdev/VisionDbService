using Microsoft.EntityFrameworkCore;
using VisionDatabase.DbModels;
using VisionModels.Messages;

namespace VisionDbService.MessageHandlers
{
    
    public class FaceDetectHandler : IMessageHandler<FaceDetect>
    {
        private readonly DbContext _dbContext;

        public FaceDetectHandler(DbContext dbc)
        {
            _dbContext = dbc;
        }

        public string Handle(FaceDetect m)
        {
            var c = new Content()
            {
                ImageId = m.ImageId,
                ContentDescription = "face",
                X = m.X,
                Y = m.Y,
                Width = m.Width,
                Height = m.Height
            };

            _dbContext.Add<Content>(c);

            _dbContext.SaveChanges();

            return c.ContentId.ToString();
        }

    }
}
