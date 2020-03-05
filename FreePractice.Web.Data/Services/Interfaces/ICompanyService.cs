using System.Threading.Tasks;

namespace FreePractice.Web.Data.Services.Interfaces
{
    public interface ICompanyService
    {
        Task<bool> Reject(int id);
    }
}