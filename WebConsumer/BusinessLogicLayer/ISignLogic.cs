using ModelLayer;
using ServiceLayer;

namespace BusinessLogicLayer
{
    public interface ISignControl
    {
        public Task<bool> CreateSign(Sign sign);
        public Task<bool> DeleteSign(int id);
        public  Task<Sign?> GetSign(int id);
        public Task<IEnumerable<Sign>> GetSigns();
        public Task<bool> UpdateSign(Sign sign, int id);
    }
}
