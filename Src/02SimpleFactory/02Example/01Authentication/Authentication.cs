using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _SimpleFactory._02Example._01Authentication
{
    // 抽象认证类
    public abstract class Authentication
    {
        public abstract void Authenticate();
    }

    // 具体认证类：用户名密码认证
    public class UsernamePasswordAuthentication : Authentication
    {
        public override void Authenticate()
        {
            Console.WriteLine("用户名账号密码认证");
        }
    }

    // 具体认证类：手机号验证码认证
    public class PhoneAuthentication : Authentication
    {
        public override void Authenticate()
        {
            Console.WriteLine("手机认证");
        }
    }

    // 具体认证类：第三方登录认证
    public class ThirdPartyAuthentication : Authentication
    {
        public override void Authenticate()
        {
            Console.WriteLine("第三方平台认证");
        }
    }
}
