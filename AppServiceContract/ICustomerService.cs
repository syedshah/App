using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppServiceContract
{
    public interface ICustomerService
    {
        /// <summary>
        /// Add a new customer
        /// </summary>
        /// <param name="firstName">first name</param>
        /// <param name="surName">sur name</param>
        /// <param name="email">email</param>
        /// <param name="dateOfBirth">date of birth</param>
        /// <param name="companyId">companyId</param>
        /// <returns>boolean</returns>
        bool AddCustomer(string firstName, string surName, string email, DateTime dateOfBirth, int companyId);
    }
}
