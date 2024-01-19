using ModelLayer;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer
{
    public class SignControl : ISignControl
    {

        private readonly ISignService _signService;

        public SignControl(ISignService signService)
        {
            _signService = signService;
        }

        public async Task<bool> CreateSign(Sign sign)
        {
            return await _signService.CreateSign(sign);
        }

        public async Task<bool> DeleteSign(int id)
        {
            return await _signService.DeleteSign(id);
        }

        public async Task<Sign?> GetSign(int id)
        {
            return await _signService.GetSign(id);
        }

        public async Task<IEnumerable<Sign>> GetSigns()
        {
            return await _signService.GetSigns();
        }

        public async Task<bool> UpdateSign(Sign sign, int id)
        {
            return await _signService.UpdateSign(sign, id);
        }
    }
}
