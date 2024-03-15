using _SimpleFactory._01ImplementationMethod;
using _SimpleFactory._02Example._01Authentication;
using _SimpleFactory._02Example._02Warehousing;

namespace _02SimpleFactory
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("-------------------------------- 登录验证 ----------------------------------"); 
            AuthenticationFactory factory = new AuthenticationFactory();
            Authentication usernamePasswordAuth = factory.CreateAuthentication<UsernamePasswordAuthentication>();
            usernamePasswordAuth.Authenticate();

            Authentication phoneVerificationAuth = factory.CreateAuthentication<PhoneAuthentication>();
            phoneVerificationAuth.Authenticate();

            Authentication thirdPartyAuth = factory.CreateAuthentication<ThirdPartyAuthentication>();
            thirdPartyAuth.Authenticate();


            Console.WriteLine("-------------------------------- 入库方式 ----------------------------------");

            WarehousingFactory factoryWarehousing = new WarehousingFactory();

            Warehousing normalWarehousing = factoryWarehousing.CreateWarehousing<NormalWarehousing>();
            normalWarehousing.Process();

            Warehousing coldChainWarehousing = factoryWarehousing.CreateWarehousing<ColdChainWarehousing>();
            coldChainWarehousing.Process();

            Warehousing hazardousMaterialWarehousing = factoryWarehousing.CreateWarehousing<HazardousMaterialWarehousing>();
            hazardousMaterialWarehousing.Process();
        }
    }
}