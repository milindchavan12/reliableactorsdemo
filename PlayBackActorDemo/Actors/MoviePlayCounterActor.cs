using Akka.Actor;
using PlayBackActorDemo.Messages;
using System.Collections.Generic;
using System;
using PlayBackActorDemo.Exceptions;

namespace PlayBackActorDemo.Actors
{
    public class MoviePlayCounterActor : ReceiveActor
    {
        private readonly Dictionary<string, int> _moviePlayCount;

        public MoviePlayCounterActor()
        {
            _moviePlayCount = new Dictionary<string, int>();

            Receive<IncrementPlayCountMessage>(message => HandleIncrementCount(message));
        }

        private void HandleIncrementCount(IncrementPlayCountMessage message)
        {
            if (_moviePlayCount.ContainsKey(message.MovieTitle))
            {
                _moviePlayCount[message.MovieTitle]++;
            }
            else
            {
                _moviePlayCount.Add(message.MovieTitle, 1);
            }

            if(_moviePlayCount[message.MovieTitle] > 3)
            {
                throw new SimulatedCorruptStateException();
            }

            if(message.MovieTitle == "Bhoot")
            {
                throw new SimulatedTerribleMovieException();
            }

            Console.WriteLine($"Movie : {message.MovieTitle} has watch count : { _moviePlayCount[message.MovieTitle]}");
        }
    }
}