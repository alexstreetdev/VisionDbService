using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using VisionDatabase.DbModels;
using VisionDbService.MessageHandlers;
using VisionModels.Messages;

namespace VisionDbService
{
    class Program
    {
        static void Main(string[] args)
        {
            var sc = new ServiceCollection();
            sc.AddSingleton<DbContext, ImageContext>();
            sc.AddTransient<IMessageHandler<Movement>, MovementHandler>();
            sc.AddTransient<IMessageHandler<FaceDetect>, FaceDetectHandler>();
            var sp = sc.BuildServiceProvider();


            Console.WriteLine("Checking database");
            var c = new ImageContext();
            c.Database.Migrate();
            Console.WriteLine("Database checked");

            Console.WriteLine("Running");
            MessageClient mq;
            try
            {
                mq = new MessageClient(sp);
                mq.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

            Console.ReadLine();

            Console.WriteLine("Stopping");
            mq.Stop();
            Console.WriteLine("Stopped");
        }
    }
}
