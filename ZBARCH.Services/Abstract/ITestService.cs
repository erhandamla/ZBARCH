namespace ZBARCH.Services.Abstract
{
    public interface ITestService
    {
        Task<int> Delete(Guid id);
        Task<List<string>> ListStringsAsync();
    }
}
