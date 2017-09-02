using Akka.Actor;
using System;
using PlayBackActorDemo.Exceptions;

namespace PlayBackActorDemo.Actors
{
    public class PlaybackStatisticsActor : ReceiveActor
    {
        public PlaybackStatisticsActor()
        {
            Context.ActorOf(Props.Create<MoviePlayCounterActor>(), "MoviePlayCounter");
        }

        protected override SupervisorStrategy SupervisorStrategy()
        {
            return new OneForOneStrategy(
                exception =>
                {
                    if(exception is SimulatedCorruptStateException)
                    {
                        return Directive.Restart;
                    }

                    if (exception is SimulatedTerribleMovieException)
                    {
                        return Directive.Resume;
                    }

                    return Directive.Restart;
                });
        }

        #region LifeCycle Events

        protected override void PreStart()
        {
            Console.WriteLine("PlaybackStatisticsActor PreStart");
        }

        protected override void PostStop()
        {
            Console.WriteLine("PlaybackStatisticsActor PostStop");
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine("PlaybackStatisticsActor PostRestart");

            base.PostRestart(reason);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("PlaybackStatisticsActor PreRestart");

            base.PreRestart(reason, message);
        }

        #endregion
    }
}