using AutoMapper;
using Core.Utilities.Results.Concrete;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.DependencyResolvers.Mapper
{
    public class MapperHelper<TSource, TDestination>
    {
        private static IMapper _mapper;

        static MapperHelper()
        {
            // IMapper nesnesini burada tanımlayabilirsiniz
            _mapper = CreateMap();
        }

        private static IMapper CreateMap()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<TSource, TDestination>();
                // Diğer eşleştirmeleri buraya ekleyebilirsiniz.
            });

            return config.CreateMapper();
        }

        public static List<TDestination> MapList(List<TSource> entities)
        {
            List<TDestination> result = new List<TDestination>();
            foreach (TSource entity in entities)
            {
                result.Add(Map(entity));
            }
            return result;
        }

        public static TDestination Map(TSource entity)
        {
            if (_mapper == null)
            {
                throw new Exception();
            }
            var res = _mapper.Map<TDestination>(entity);
            return res;
        }
    }
}