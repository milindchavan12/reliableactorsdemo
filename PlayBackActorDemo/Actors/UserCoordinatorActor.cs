using Akka.Actor;
using PlayBackActorDemo.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlayBackActorDemo.Actors
{
    public class UserCoordinatorActor : ReceiveActor
    {
        private readonly Dictionary<int, IActorRef> _users;

        public UserCoordinatorActor()
        {
            _users = new Dictionary<int, IActorRef>();

            Receive<PlayMovieMessage>(message =>
            {
                CreateChildActorIfNotExists(message.UserId);

                IActorRef childActorRef = _users[message.UserId];

                childActorRef.Tell(message);
            });

            Receive<StopMovieMessage>(message =>
            {
                CreateChildActorIfNotExists(message.UserId);

                IActorRef childActorRef = _users[message.UserId];

                childActorRef.Tell(message);
            });
        }

        private void CreateChildActorIfNotExists(int userId)
        {
            if(!_users.ContainsKey(userId))
            {
                IActorRef newChildActorRef = Context.ActorOf(Props.Create(() => new UserActor(userId)), "User" + userId);

                _users.Add(userId, newChildActorRef);

                Console.WriteLine($"New UserCoordinatorActor created for UserId {userId}. Total User Count : {_users.Count}");
            }
        }

        #region LifeCycle Events

        protected override void PreStart()
        {
            Console.WriteLine("UserCoordinatorActor PreStart");
        }

        protected override void PostStop()
        {
            Console.WriteLine("UserCoordinatorActor PostStop");
        }

        protected override void PostRestart(Exception reason)
        {
            Console.WriteLine("UserCoordinatorActor PostRestart");

            base.PostRestart(reason);
        }

        protected override void PreRestart(Exception reason, object message)
        {
            Console.WriteLine("UserCoordinatorActor PreRestart");

            base.PreRestart(reason, message);
        }

        #endregion
    }
}
