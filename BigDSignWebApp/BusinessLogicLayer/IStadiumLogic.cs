using ModelLayer;
using ServiceLayer;

namespace BusinessLogicLayer
{
    public interface IStadiumControl
    {
        public Task<bool> CreateStadium(Stadium stadium);
        public Task<bool> DeleteStadium(int id);
        public  Task<Stadium?> GetStadium(int id);
        public Task<IEnumerable<Stadium>> GetStadiums();
        public Task<bool> UpdateStadium(Stadium stadium, int id);
    }
}
