using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifeGame.EventStore.Implementation.Test
{
    using LifeGame.CommandHandlers;
    using LifeGame.Commands;
    using LifeGame.EventStore.Aggregate;
    using LifeGame.EventStore.Storage.Memento;

    class Entity : BaseAggregateRoot<IDomainEvent>, IOrginator
    {
        public string Name { get; set; }

        public Entity (): this (Guid.NewGuid(), 0 )
        {
        }

        public Entity(int version)
            : this(Guid.NewGuid(), version)
        {
        }

        public Entity(Guid id, int version) 
        {
            this.InternalRegisterEvents();   
            Apply(new EntityCreated(id, version));
        }

        private void InternalRegisterEvents()
        {
            RegisterEvent<EntityCreated>(OnCreatedEntity);
            RegisterEvent<EntityChangedNameEvent>(OnChangeName);
        }

        private void OnChangeName(EntityChangedNameEvent @event)
        {
            this.Name = @event.Name;
        }

        private void OnCreatedEntity(EntityCreated @event)
        {
            this.Id = @event.IdEntityCreated;
            this.Version = @event.VesionEntity;
        }

        public Entity ChangeName(string name)
        {
            Apply(new EntityChangedNameEvent(name) );
            return this;
        }

        public IMemento CreateMemento()
        {
            return new EntityMemento(this.Id, this.Version, this.Name);
        }

        public void SetMemento(IMemento memento)
        {
            var entityMemento = memento as EntityMemento;
            this.Id = entityMemento.Id;
            this.Version = entityMemento.Version;
            this.Name = entityMemento.Name;
        }
    }

    internal class EntityCreated : IDomainEvent
    {
        public Guid Id { get; private set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }

        public EntityCreated(Guid id, int version)
        {
            this.Id = Guid.NewGuid();
            this.IdEntityCreated = id;
            this.VesionEntity = version;
        }

        public int VesionEntity { get; set; }

        public Guid IdEntityCreated { get; set; }
    }



    internal class EntityChangedNameEvent : IDomainEvent
    {
        public EntityChangedNameEvent(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        public string Name { get; set; }
        public Guid Id { get; private set; }
        public Guid AggregateId { get; set; }
        public int Version { get; set; }
    }

    internal class EntityMemento : IMemento
    {
        public EntityMemento(Guid id, int version, string name)
        {
            this.Id = id;
            this.Version = version;
            this.Name = name;
        }

        public int Version { get; set; }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
    internal class EntityIsChanginNameHandler : ICommandHandler<EntityChangeNameCommand>
    {
        public EntityIsChanginNameHandler(IDomainRepository<IDomainEvent> repository)
        {
            this.Repository = repository;
        }

        public IDomainRepository<IDomainEvent> Repository { get; set; }

        public void Execute(EntityChangeNameCommand command)
        {
            var entity = this.Repository.GetById<Entity>(command.Id);
            entity.ChangeName(command.Name);
        }
    }

    internal class EntityChangeNameCommand : ICommand
    {
        public Guid Id { get; private set; }

        public EntityChangeNameCommand(Guid id, string newName)
        {
            this.Id = id;
            this.Name = newName;
        }

        public string Name { get; set; }
    }
}
