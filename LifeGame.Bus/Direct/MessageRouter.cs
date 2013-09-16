namespace LifeGame.Bus.Direct
{
    using System;
    using System.Collections.Generic;

    public class MessageRouter : IRouteMessages
    {
        private readonly IDictionary<Type, ICollection<Action<object>>> routes;

        public MessageRouter()
        {
            this.routes = new Dictionary<Type, ICollection<Action<object>>>();
        }

        public void Register<TMessage>(Action<TMessage> route) where TMessage : class
        {
            var routingKey = typeof(TMessage);
            ICollection<Action<object>> routes;

            if (!this.routes.TryGetValue(routingKey, out routes))
                this.routes[routingKey] = routes = new LinkedList<Action<object>>();

            routes.Add(message => route(message as TMessage));
        }

        public void Route(object message)
        {
            ICollection<Action<object>> routes;

            if (!this.routes.TryGetValue(message.GetType(), out routes))
                throw new RouteNotRegisteredException(message.GetType());

            foreach (var route in routes)
                route(message);
        }
    }
}