using _SimpleFactory._01ImplementationMethod;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _SimpleFactory._02Example._01Authentication
{
    // 简单工厂类
    public class AuthenticationFactory
    {
        public T CreateAuthentication<T>() where T : Authentication, new()
        {
            return new T();
        }
    }
}
