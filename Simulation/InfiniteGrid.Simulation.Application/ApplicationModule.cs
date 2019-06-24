using Autofac;

namespace InfiniteGrid.Simulation.Application
{
    public class ApplicationModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterType<TextRenderer>().As<IGridRenderer>();
            builder.RegisterType<GridRendererFactory>().As<IGridRendererFactory>();
        }
    }
}