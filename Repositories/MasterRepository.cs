using Dapper;
using Digista_Web_Api.Context;
using Digista_Web_Api.DTOModels;
using Digista_Web_Api.IRepositories;
using Digista_Web_Api.Models;
using System.Runtime.InteropServices;

namespace Digista_Web_Api.Repositories
{
    public class MasterRepository: IMasterRepository
    {
        private readonly DapperContext _dapperContext;

        public MasterRepository(DapperContext dapperContext)
        {
            _dapperContext = dapperContext;
        }

        public async Task<int> InsertStateAsync(string state)
        {
            try
            {
                var query = "INSERT INTO state (state) VALUES (@State); SELECT LAST_INSERT_ID();";

                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteScalarAsync<int>(query, new { State = state });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while inserting state.", ex);
            }
        }

        public async Task<int> InsertDistrictAsync(DistrictDTOModel districtDTOModel)
        {
            try
            {
                var query = "INSERT INTO district (state_id, district) VALUES (@StateId, @District); SELECT LAST_INSERT_ID();";

                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.ExecuteScalarAsync<int>(query, new { StateId = districtDTOModel.state_id, District = districtDTOModel.district });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while inserting district.", ex);
            }
        }

        public async Task<IEnumerable<StateModel>> ListStatesAsync()
        {
            try
            {
                var query = "SELECT * FROM state";

                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.QueryAsync<StateModel>(query);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching states.", ex);
            }
        }

        public async Task<IEnumerable<DistrictModel>> GetDistrictByStateIdAsync(int stateId)
        {
            try
            {
                var query = "SELECT * FROM district WHERE state_id = @stateId";

                using (var connection = _dapperContext.CreateConnection())
                {
                    return await connection.QueryAsync<DistrictModel>(query, new { stateId });
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error occurred while fetching districts by state ID.", ex);
            }
        }

    }
}
