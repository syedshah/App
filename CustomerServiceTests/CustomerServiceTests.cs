

using System;
using AppRepository;
using AppService;
using AppServiceContract;
using Moq;
using NUnit.Framework;

namespace AppServiceTests
{
    [Category("Unit")]
    [TestFixture]
    public class CustomerServiceTests
    {
        private Mock<ICompanyRepository> _companyRepository;
        private ICustomerService _customerService;

        [SetUp]
        public void SetUp()
        {
            this._companyRepository = new Mock<ICompanyRepository>();
            this._customerService = new CustomerService(_companyRepository.Object);
            
        }

        [Test]
        public void GivenValidCustomerData_WhenITryToAddCustomer_ItIsCreated()
        {
            this._customerService.AddCustomer(It.IsAny<String>(), It.IsAny<String>(), It.IsAny<String>(),
                It.IsAny<DateTime>(), It.IsAny<int>());

        }
    }
}
