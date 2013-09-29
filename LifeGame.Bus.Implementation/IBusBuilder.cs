namespace LifeGame.Bus.Implementation
{
    public interface IBusBuilder
    {
        NServiceBus.IBus GetBus();
    }
}