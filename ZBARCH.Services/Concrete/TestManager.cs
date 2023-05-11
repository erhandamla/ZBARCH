using ZBARCH.DataAccess.Abstract;
using ZBARCH.Services.Abstract;

namespace ZBARCH.Services.Concrete
{
    public class TestManager : ITestService
    {
        private readonly ITestDal _testDal;
        public TestManager(ITestDal testDal)
        {
            _testDal = testDal;
        }
        public async Task<int> Delete(Guid id)
        {
            var entry = await _testDal.GetById(id);
            var result = await _testDal.Delete(entry);

            return result;
        }

        public async Task<List<string>> ListStringsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
