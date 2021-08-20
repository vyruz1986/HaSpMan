using AutoMapper;

using Commands.MapperProfiles;

using Xunit;

namespace Commands.Test
{
    public class MappingTests
    {
        private readonly IMapper _mapper;
        public MappingTests()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<MemberProfile>();
            });
            _mapper = new Mapper(config);
        }

        [Fact]
        public void AutoMapperAssertions()
        {
            _mapper.ConfigurationProvider.AssertConfigurationIsValid();
        }
    }
}
