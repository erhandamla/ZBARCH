using ZBARCH.Core.EFCore;
using ZBARCH.DataAccess.Abstract;
using ZBARCH.Entities.Concrete;

namespace ZBARCH.DataAccess.Concrete.EFCore
{
    public class TestDal : EFCoreRepository<Test, TestDBContext>, ITestDal
    {
    }
}
