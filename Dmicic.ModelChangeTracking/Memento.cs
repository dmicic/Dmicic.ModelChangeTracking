using EmitMapper;
using EmitMapper.Mappers;
using EmitMapper.MappingConfiguration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ModelChangeTracking
{
    public class Memento<T> : IDisposable
    {
        private string[] PropertiesToIgnore { get; set; }
        private T Backup { get; set; }
        private T Originator { get; set; }

        private static readonly Lazy<ObjectsMapperBaseImpl> ObjectMapper =
            new Lazy<ObjectsMapperBaseImpl>(
                    () => ObjectMapperManager.DefaultInstance.GetMapper<T, T>().MapperImpl);

        private ObjectsMapperBaseImpl Mapper
        {
            get { return Memento<T>.ObjectMapper.Value; }
        }

        public void Save(object obj)
        {
            this.Backup = (T)this.Mapper.Map(obj);
            this.Originator = (T)obj;
        }

        public void Restore()
        {
            this.Mapper.Map(this.Backup, this.Originator, null);
        }

        public void Dispose()
        {
            this.Backup = default(T);
        }
    }
}
