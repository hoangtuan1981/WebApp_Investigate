using WebApp_Investigate.Intefaces;

namespace WebApp_Investigate.Services
{
    public class OneClassTwoInterfacesService : IReader, IHelper
    {
        public int TotalLines()
        {
            return 100;
        }

        public string GetName()
        {
            return $"Class name: {this.GetType().Name}";
        }
    }
}
