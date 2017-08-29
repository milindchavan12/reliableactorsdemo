using System;
using Akka.Actor;
using ActorSystemDemo.Messages;

namespace ActorSystemDemo.Actors
{
    public class UserActor : ReceiveActor
    {
        private string _currentlyWatching;

        public UserActor()
        {
            Console.WriteLine("Creating UserActor");

            //Receive<PlayMovieMessage>(message => HandlePlayMovieMessage(message));
            //Receive<StopMovieMessage>(message => HandleStopMovieMessage());

            Console.WriteLine("Setting the initial behaviour to stopped");
        }

        private void Playing()
        {
            Receive<PlayMovieMessage>(message => Console.WriteLine("Error : Can not play movies before stopping another one"));
            Receive<StopMovieMessage>(message => StopPlayingMovie());

            Console.WriteLine("UserActor is now playing");
        }

        private void Stopped()
        {
            Receive<PlayMovieMessage>(message => StartPlayingMovie(message.MovieTitle));
            Receive<StopMovieMessage>(message => Console.WriteLine("Error : Can not stop if nothing is playing"));

            Console.WriteLine("UserActor is now stopped");
        }

        private void StartPlayingMovie(string movieTitle)
        {
            _currentlyWatching = movieTitle;

            Console.WriteLine($"You are currently watching {movieTitle}");

            Become(Playing);
        }

        private void StopPlayingMovie()
        {
            Console.WriteLine($"Currently watching movie {_currentlyWatching} is stopped.");

            _currentlyWatching = null;

            Become(Stopped);
        }

        protected override void PreStart()
        {
            Console.WriteLine("Actor PreStart");
        }

        protected override void PostStop()
        {
            Console.WriteLine("Actor PostStop");
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine("Actor PostRestart");

            base.PostRestart(reason);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("Actor PreRestart");

            base.PreRestart(reason, message);
        }
    }
}
