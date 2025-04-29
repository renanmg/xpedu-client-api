using XPEdu.Client.Api.Models.DTOs;

namespace XPEdu.Client.Api.Infra.Repositories
{
    public interface IClientRepository
    {
        Task<IEnumerable<ClientDTO>> GetAllAsync();
        Task<ClientDTO> GetByIdAsync(long id);
        Task<long> AddAsync(NewClientDTO newClientDTO);
        Task UpdateAsync(long id, NewClientDTO newClientDTO);
        Task DeleteAsync(long id);
        Task<long> GetTotalRecords();
        Task<IEnumerable<ClientDTO>> GetByNameFilter(string filter);

    }
}
