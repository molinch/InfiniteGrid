namespace InfiniteGrid.Simulation.Application
{
    public interface IGridRendererFactory
    {
        IGridRenderer Get(string outputType);
    }
}