using ZBARCH.Core;
using ZBARCH.Entities.Concrete;

namespace ZBARCH.DataAccess.Abstract
{
    public interface ITestDal : IRepository<Test>
    {
        //custom operation like call stored procedure, or views, or join queries
    }
}
