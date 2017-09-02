using Akka.Actor;
using PlayBackActorDemo.Actors;
using PlayBackActorDemo.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PlayBackActorDemo
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            Console.WriteLine("Creating MovieStreamingActorSystem");
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");

            Console.WriteLine("Creating Actor supervisory hierarchy");
            MovieStreamingActorSystem.ActorOf(Props.Create<PlaybackActor>(), "Playback");

            do
            {
                ShortPause();

                Console.WriteLine();
                Console.WriteLine("Please enter the command :");

                var command = Console.ReadLine();

                if(command.StartsWith("play"))
                {
                    int userId = int.Parse(command.Split(',')[1]);
                    var movieTitle = command.Split(',')[2];

                    var message = new PlayMovieMessage(movieTitle, userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("stop"))
                {
                    int userId = int.Parse(command.Split(',')[1]);

                    var message = new StopMovieMessage(userId);
                    MovieStreamingActorSystem.ActorSelection("/user/Playback/UserCoordinator").Tell(message);
                }

                if (command.StartsWith("exit"))
                {
                    MovieStreamingActorSystem.Terminate();
                    Console.WriteLine("Actor system shutting down");
                    Console.ReadLine();
                    Environment.Exit(1);
                }
            } while (true);
        }

        private static void ShortPause()
        {
            Thread.Sleep(1000);
        }
    }
}
