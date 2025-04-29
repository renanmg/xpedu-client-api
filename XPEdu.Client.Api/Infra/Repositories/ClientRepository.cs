using Dapper;
using System.Data;
using XPEdu.Client.Api.Models.DTOs;

namespace XPEdu.Client.Api.Infra.Repositories
{
    public class ClientRepository : IClientRepository
    {
        private readonly IDbConnection _dbConnection;

        public ClientRepository(IDbConnection dbConnection)
        {
            _dbConnection = dbConnection;
        }

        public async Task<long> AddAsync(NewClientDTO newClientDTO)
        {
            var sql = "INSERT INTO Clientes (Nome, Email, Telefone, Endereco) VALUES (@Name, @Email, @Phone, @Address); SELECT LAST_INSERT_ID();";
            return await _dbConnection.QuerySingleAsync<int>(sql, newClientDTO);
        }

        public async Task DeleteAsync(long id)
        {
            var sql = "DELETE FROM Clientes WHERE Id = @Id";
            await _dbConnection.ExecuteAsync(sql, new { Id = id });
        }

        public async Task<IEnumerable<ClientDTO>> GetAllAsync()
        {
            var sql = "SELECT Id, Nome as Name, Email, Telefone as Phone, Endereco as Address FROM Clientes";
            return await _dbConnection.QueryAsync<ClientDTO>(sql);
        }

        public async Task<ClientDTO> GetByIdAsync(long id)
        {
            var sql = "SELECT Id, Nome as Name, Email, Telefone as Phone, Endereco as Address FROM Clientes WHERE Id = @Id";
            return await _dbConnection.QueryFirstOrDefaultAsync<ClientDTO>(sql, new { Id = id });
        }

        public async Task<IEnumerable<ClientDTO>> GetByNameFilter(string filter)
        {
            var sql = "SELECT Id, Nome as Name, Email, Telefone as Phone, Endereco as Address FROM Clientes WHERE Nome LIKE @Filter";
            return await _dbConnection.QueryAsync<ClientDTO>(sql, new { Filter = filter });
        }

        public async Task<long> GetTotalRecords()
        {
            var sql = "SELECT COUNT(*) FROM Clientes";
            return await _dbConnection.ExecuteScalarAsync<long>(sql);
        }

        public async Task UpdateAsync(long id, NewClientDTO newClientDTO)
        {
            var sql = "UPDATE Clientes SET Nome = @Name, Email = @Email, Telefone = @Phone, Endereco = @Address WHERE Id = @id";
            var dbParams = new { newClientDTO.Name, newClientDTO.Email, newClientDTO.Phone, newClientDTO.Address, id };
            await _dbConnection.ExecuteAsync(sql, dbParams);
        }
    }
}
