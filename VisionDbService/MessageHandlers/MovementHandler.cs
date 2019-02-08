using Microsoft.EntityFrameworkCore;
using VisionDatabase.DbModels;
using VisionModels.Messages;

namespace VisionDbService.MessageHandlers
{
    public class MovementHandler : IMessageHandler<Movement>
    {
        private readonly DbContext _dbContext;

        public MovementHandler(DbContext dbc)
        {
            _dbContext = dbc;
        }

        public string Handle(Movement m)
        {
            var img = new Image()
            {
                ImageId = m.ImageId,
                Filename = m.Filename,
                EventTime = m.EventTime,
                CorrelationId = m.CorrelationId,
                SequenceNumber = m.SequenceNumber,
                ImageLocation = m.Url,
                Source = m.CameraId
            };

            foreach (Region r in m.Regions)
            {
                var c = new Content()
                {
                    ContentDescription = "movement",
                    X = r.X,
                    Y = r.Y,
                    Width = r.Width,
                    Height = r.Height
                };
                img.Contents.Add(c);
            }

            _dbContext.Add<Image>(img);

            _dbContext.SaveChanges();


            return string.Empty;
        }

    }
}