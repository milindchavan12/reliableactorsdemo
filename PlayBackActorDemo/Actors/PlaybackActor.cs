using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBackActorDemo.Actors
{
    public class PlaybackActor : ReceiveActor
    {
        public PlaybackActor()
        {
            Context.ActorOf(Props.Create<UserCoordinatorActor>(), "UserCoordinator");
            Context.ActorOf(Props.Create<PlaybackStatisticsActor>(), "PlaybackStatistics");
        }

        #region LifeCycle Events

        protected override void PreStart()
        {
            Console.WriteLine("PlaybackActor PreStart");
        }

        protected override void PostStop()
        {
            Console.WriteLine("PlaybackActor PostStop");
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine("PlaybackActor PostRestart");

            base.PostRestart(reason);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("PlaybackActor PreRestart");

            base.PreRestart(reason, message);
        }

        #endregion
    }
}
