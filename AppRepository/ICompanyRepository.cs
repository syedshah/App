using Entities;

namespace AppRepository
{
    public interface ICompanyRepository
    {
        Company GetById(int companyId);
    }
}
