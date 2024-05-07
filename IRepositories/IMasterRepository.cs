using Digista_Web_Api.DTOModels;
using Digista_Web_Api.Models;
using Org.BouncyCastle.Tsp;

namespace Digista_Web_Api.IRepositories
{
    public interface IMasterRepository
    {
        public Task<int> InsertStateAsync(string state_name);
        public Task<int> InsertDistrictAsync(DistrictDTOModel districtDTOModel);
        public Task<IEnumerable<StateModel>> ListStatesAsync();
        public  Task<IEnumerable<DistrictModel>> GetDistrictByStateIdAsync(int state_id);
    }
}
