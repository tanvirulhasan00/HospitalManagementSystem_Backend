using HospitalManagementSystem.Models.GenericModels;

namespace HospitalManagementSystem.Services.IService;

public interface IService<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync(GenericServiceRequest<T> request);
    Task<T> GetAsync(GenericServiceRequest<T> request);
    Task AddAsync(T entity);
    Task<bool> AnyAsync(GenericServiceRequest<T> request);
    void Remove(T entity);
    void RemoveRange(IEnumerable<T> entities);
}