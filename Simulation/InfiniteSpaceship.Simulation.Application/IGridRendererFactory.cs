namespace InfiniteSpaceship.Simulation.Application
{
    public interface IGridRendererFactory
    {
        IGridRenderer Get(string outputType);
    }
}