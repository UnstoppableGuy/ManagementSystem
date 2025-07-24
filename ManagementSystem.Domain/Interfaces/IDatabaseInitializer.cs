using System.Threading.Tasks;

namespace ManagementSystem.Domain.Interfaces
{
    public interface IDatabaseInitializer
    {
        void Initialize();
        Task InitializeAsync();
    }
}