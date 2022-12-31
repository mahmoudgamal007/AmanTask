namespace AmanTaskBackEnd.RepoInterfaces
{
    public interface IBaseRepo<T1,T2,T3>
    {
        public Task<T2> GetAll();
        public Task<T1> GetById(int Id);
        public Task<T1> Create(T3 model);
        public Task<T1> Update(int Id, T3 model);
        public Task<T1> Delete(int Id);
        public bool IsExists(int Id);
    }
}
