using ModelLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class StadiumControl : IStadiumControl
    {

        private readonly IStadiumService _stadiumService;

        public StadiumControl(IStadiumService stadiumService)
        {
            _stadiumService = stadiumService;
        }
        public async Task<bool> CreateStadium(Stadium stadium)
        {
            return await _stadiumService.CreateStadium(stadium);
        }

        public async Task<bool> DeleteStadium(int id)
        {
            return await _stadiumService.DeleteStadium(id);
        }

        public async Task<IEnumerable<Stadium>> GetStadiums()
        {
            return await _stadiumService.GetStadiums();
        }

        public async Task<Stadium?> GetStadium(int id)
        {
            return await _stadiumService.GetStadium(id);
        }

        public async Task<bool> UpdateStadium(Stadium stadium, int id)
        {
            return await _stadiumService.UpdateStadium(stadium, id);
        }
    }
}
