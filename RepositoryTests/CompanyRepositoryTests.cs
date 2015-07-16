using System.Transactions;
using AppRepository;
using Entities;
using FluentAssertions;
using Moq;
using NUnit.Framework;
using System.Configuration;

namespace RepositoryTests
{
    [Category("Integration")]
    [TestFixture]
    public class CompanyRepositoryTests
    {
        private ICompanyRepository _companyRepository;
        private string _connectionString;

        [SetUp]
        public void SetUp()
        {
           // this._companyRepository = new CompanyRepository();
            var connectionString = ConfigurationManager.ConnectionStrings["appDatabase"];
            if (connectionString!=null)
            {
               _connectionString= connectionString.ConnectionString;
            }
            this._companyRepository = new CompanyRepository(_connectionString);   
        }


        [Test]
        public void GivenACompanyId_WhenIAskForCompany_IGetTheCompany()
        {
            var result = this._companyRepository.GetById(It.IsAny<int>());
            result.Should().NotBeNull();
            result.Should().BeOfType<Company>();
        }
    }
}
