using System.Collections;

namespace _06Prototype._01ImplementationMethod
{
    // 原型类
    public class Prototype<T> : IPrototype<Prototype<T>>, IEnumerable<KeyValuePair<string, T>> where T : new()
    {
        private readonly Dictionary<string, T> properties = new();

        public T this[string key]
        {
            get => properties.TryGetValue(key, out var value) ? value! : default!;
            set => properties[key] = value;
        }

        // 浅拷贝方法
        public Prototype<T> ShallowCopy()
        {
            return (Prototype<T>)this.MemberwiseClone();
        }

        // 深拷贝方法
        public Prototype<T> DeepCopy()
        {
            var newPrototype = new Prototype<T>();
            foreach (var entry in properties)
            {
                if (entry.Value is IPrototype<T> prototype)
                {
                    newPrototype.properties[entry.Key] = prototype.DeepCopy();
                }
                else
                {
                    newPrototype.properties[entry.Key] = entry.Value;
                }
            }

            return newPrototype;
        }

        // 实现 IEnumerable<KeyValuePair<string, T>> 接口
        public IEnumerator<KeyValuePair<string, T>> GetEnumerator()
        {
            return properties.GetEnumerator();
        }

        // 显式实现非泛型 IEnumerable 接口
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
