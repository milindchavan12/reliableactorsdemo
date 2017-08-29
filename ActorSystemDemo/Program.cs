using ActorSystemDemo.Actors;
using ActorSystemDemo.Messages;
using Akka.Actor;
using System;

namespace ActorSystemDemo
{
    class Program
    {
        private static ActorSystem MovieStreamingActorSystem;

        static void Main(string[] args)
        {
            MovieStreamingActorSystem = ActorSystem.Create("MovieStreamingActorSystem");
            Console.WriteLine("Actor System created");

            Props userActorProps = Props.Create<UserActor>();
            IActorRef userActorRef = MovieStreamingActorSystem.ActorOf(userActorProps, "UserActor");

            Console.ReadKey();
            Console.WriteLine("Playing first movie");
            userActorRef.Tell(new PlayMovieMessage("Movie 1", 42));

            Console.ReadKey();
            Console.WriteLine("Playing second movie");
            userActorRef.Tell(new PlayMovieMessage("Movie 2", 62));

            Console.ReadKey();
            Console.WriteLine("Stopping first movie");
            userActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            Console.WriteLine("Stopping second movie");
            userActorRef.Tell(new StopMovieMessage());

            Console.ReadKey();
            MovieStreamingActorSystem.Terminate();
        }
    }
}
