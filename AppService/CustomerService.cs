using AppRepository;
using AppServiceContract;
using Entities;
using System;

namespace AppService
{
    public class CustomerService : ICustomerService
    {
        private readonly ICompanyRepository _companyRepository;

        public CustomerService( ICompanyRepository companyRepository)
        {
            _companyRepository = companyRepository;
        }

        public bool AddCustomer(string firstName, string surName, string email, DateTime dateOfBirth, int companyId)
        {
            if (!ValidateInput(firstName, surName, email, dateOfBirth)) return false;

            var company = _companyRepository.GetById(companyId);
            if (company == null) return false; //Todo: create a new company

            var customer = new Customer
            {
                Company = company,
                DateOfBirth = dateOfBirth,
                EmailAddress = email,
                Firstname = firstName,
                Surname = surName
            };

            switch (company.Name.ToLower())
            {
                case "veryimportantclient":
                    customer.HasCreditLimit = false;
                    break;
                case "importantclient":
                    customer.HasCreditLimit = true;
                    using (var customerCreditService = new CustomerCreditServiceClient())
                    {
                        var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                        creditLimit = creditLimit * 2;
                        customer.CreditLimit = creditLimit;
                    }
                    break;
                default:
                    customer.HasCreditLimit = true;
                    using (var customerCreditService = new CustomerCreditServiceClient())
                    {
                        var creditLimit = customerCreditService.GetCreditLimit(customer.Firstname, customer.Surname, customer.DateOfBirth);
                        customer.CreditLimit = creditLimit;
                    }
                    break;
            }

            if (customer.HasCreditLimit && customer.CreditLimit < 500)
            {
                return false;
            }

            CustomerDataAccess.AddCustomer(customer);

            return true;
        }

        private static bool ValidateInput(string firname, string surname, string email, DateTime dateOfBirth)
        {
            if (string.IsNullOrEmpty(firname) || string.IsNullOrEmpty(surname)) return false;

            if (!email.Contains("@") && !email.Contains(".")) return false;
            
            var now = DateTime.Now;
            var age = now.Year - dateOfBirth.Year;
            if (now.Month < dateOfBirth.Month || (now.Month == dateOfBirth.Month && now.Day < dateOfBirth.Day)) age--;

            return age >= 21;
        }
    }
}
