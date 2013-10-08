// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DomainEntity.cs" company="Abraham Alcaina">
//   
// </copyright>
// <summary>
//   The entity.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace LifeGame.EventStore.Implementation.Test
{
    using System;

    using LifeGame.CommandHandlers;
    using LifeGame.Commands;
    using LifeGame.EventStore.Aggregate;
    using LifeGame.EventStore.Storage.Memento;

    /// <summary>
    /// The entity.
    /// </summary>
    internal class Entity : BaseAggregateRoot<IDomainEvent>, IOrginator
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        public Entity()
            : this(Guid.NewGuid(), 0)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="version">
        /// The version.
        /// </param>
        public Entity(int version)
            : this(Guid.NewGuid(), version)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Entity"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        public Entity(Guid id, int version)
        {
            this.InternalRegisterEvents();
            this.Apply(new EntityCreated(id, version));
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The change name.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="Entity"/>.
        /// </returns>
        public Entity ChangeName(string name)
        {
            this.Apply(new EntityChangedNameEvent(name));
            return this;
        }

        /// <summary>
        /// The create memento.
        /// </summary>
        /// <returns>
        /// The <see cref="IMemento"/>.
        /// </returns>
        public IMemento CreateMemento()
        {
            return new EntityMemento(this.Id, this.Version, this.Name);
        }

        /// <summary>
        /// The set memento.
        /// </summary>
        /// <param name="memento">
        /// The memento.
        /// </param>
        public void SetMemento(IMemento memento)
        {
            var entityMemento = memento as EntityMemento;
            this.Id = entityMemento.Id;
            this.Version = entityMemento.Version;
            this.Name = entityMemento.Name;
        }

        #endregion

        #region Methods

        /// <summary>
        /// The internal register events.
        /// </summary>
        private void InternalRegisterEvents()
        {
            this.RegisterEvent<EntityCreated>(this.OnCreatedEntity);
            this.RegisterEvent<EntityChangedNameEvent>(this.OnChangeName);
        }

        /// <summary>
        /// The on change name.
        /// </summary>
        /// <param name="event">
        /// The event.
        /// </param>
        private void OnChangeName(EntityChangedNameEvent @event)
        {
            this.Name = @event.Name;
        }

        /// <summary>
        /// The on created entity.
        /// </summary>
        /// <param name="event">
        /// The event.
        /// </param>
        private void OnCreatedEntity(EntityCreated @event)
        {
            this.Id = @event.IdEntityCreated;
            this.Version = @event.VesionEntity;
        }

        #endregion
    }

    /// <summary>
    /// The entity created.
    /// </summary>
    internal class EntityCreated : IDomainEvent
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCreated"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        public EntityCreated(Guid id, int version)
        {
            this.Id = Guid.NewGuid();
            this.IdEntityCreated = id;
            this.VesionEntity = version;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the aggregate id.
        /// </summary>
        public Guid AggregateId { get; set; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the id entity created.
        /// </summary>
        public Guid IdEntityCreated { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public int Version { get; set; }

        /// <summary>
        /// Gets or sets the vesion entity.
        /// </summary>
        public int VesionEntity { get; set; }

        #endregion
    }

    /// <summary>
    /// The entity changed name event.
    /// </summary>
    internal class EntityChangedNameEvent : IDomainEvent
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityChangedNameEvent"/> class.
        /// </summary>
        /// <param name="name">
        /// The name.
        /// </param>
        public EntityChangedNameEvent(string name)
        {
            this.Id = Guid.NewGuid();
            this.Name = name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the aggregate id.
        /// </summary>
        public Guid AggregateId { get; set; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public int Version { get; set; }

        #endregion
    }

    /// <summary>
    /// The entity memento.
    /// </summary>
    internal class EntityMemento : IMemento
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityMemento"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="version">
        /// The version.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        public EntityMemento(Guid id, int version, string name)
        {
            this.Id = id;
            this.Version = version;
            this.Name = name;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the version.
        /// </summary>
        public int Version { get; set; }

        #endregion
    }

    /// <summary>
    /// The entity is changin name handler.
    /// </summary>
    internal class EntityIsChanginNameHandler : ICommandHandler<EntityChangeNameCommand>
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityIsChanginNameHandler"/> class.
        /// </summary>
        /// <param name="repository">
        /// The repository.
        /// </param>
        public EntityIsChanginNameHandler(IDomainRepository<IDomainEvent> repository)
        {
            this.Repository = repository;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets or sets the repository.
        /// </summary>
        public IDomainRepository<IDomainEvent> Repository { get; set; }

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// The execute.
        /// </summary>
        /// <param name="command">
        /// The command.
        /// </param>
        public void Execute(EntityChangeNameCommand command)
        {
            var entity = this.Repository.GetById<Entity>(command.Id);
            entity.ChangeName(command.Name);
        }

        #endregion
    }

    /// <summary>
    /// The entity change name command.
    /// </summary>
    internal class EntityChangeNameCommand : ICommand
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityChangeNameCommand"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <param name="newName">
        /// The new name.
        /// </param>
        public EntityChangeNameCommand(Guid id, string newName)
        {
            this.Id = id;
            this.Name = newName;
        }

        #endregion

        #region Public Properties

        /// <summary>
        /// Gets the id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        #endregion
    }
}