using AutoMapper;

namespace Shared.Mapping
{
    public static class MappingConfiguration
    {
        public static IMapper Mapper { get; private set; }

        public static MapperConfiguration MapperConfiguration { get; private set; }

        public static void Init()
        {
            var config = MapConfiguration();
            MapperConfiguration = config;
            Mapper = config.CreateMapper();
        }

        private static MapperConfiguration MapConfiguration()
        {
            var mapperConfigurations = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(s => s.GetTypes())
                .Where(p => typeof(IMapFrom).IsAssignableFrom(p) && p.GetInterfaces().Contains(typeof(IMapFrom)));

            var mapperConfigurationsInstances = mapperConfigurations
                .Select(dependencyRegistrar => (IMapFrom)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(x => x.Order);

            var mapperConfigs = new MapperConfiguration(cfg =>
            {
                foreach (var instance in mapperConfigurationsInstances)
                {
                    cfg.AddProfile(instance.GetType());
                }
            });

            return mapperConfigs;
        }
    }
}
