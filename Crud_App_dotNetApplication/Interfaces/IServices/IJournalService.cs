using System.Collections.Generic;
using System.Threading.Tasks;
using Crud_App_dotNetApplication.DTOs;

namespace Crud_App_dotNetApplication.Interfaces.IServices
{
    public interface IJournalService
    {
        Task<JournalDTO> AddJournalAsync(JournalDTO journalDto);

        Task<IEnumerable<JournalDTO>> GetAllJournalsAsync();

        Task<JournalDTO> GetJournalByIdAsync(int id);

        Task<JournalDTO> UpdateJournalAsync(int id, JournalDTO journalDto);

        Task<bool> DeleteJournalAsync(int id);
    }
}