using AutoMapper;
using Chat.Application.Mapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chat.Test.Helpers
{
    public class MapperFixture
    {
        public MapperFixture()
        {
            var configuration = new MapperConfiguration(cfg => {
                cfg.AddProfile<Core>();
            });

            Mapper = configuration.CreateMapper();
        }

        /// <summary>
        /// Instance to map objects.
        /// </summary>
        public IMapper Mapper { get; }
    }
}
