using System;

using AutoMapper;

using Queries.MapperProfiles;

using Xunit;

namespace Queries.Test
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
