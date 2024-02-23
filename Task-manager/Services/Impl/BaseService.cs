using Task_Management.Services.Interfaces;

namespace Task_Management.Services.Impl
{
    public class BaseService : IBaseService
    {
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
