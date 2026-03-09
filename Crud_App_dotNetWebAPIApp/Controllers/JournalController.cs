using Microsoft.AspNetCore.Mvc;
using Crud_App_dotNetApplication.Interfaces.IServices;

namespace Crud_App_dotNetAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;

        public JournalController(IJournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateJournal(JournalDTO journalDto)
        {
            var result = await _journalService.AddJournalAsync(journalDto);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllJournals()
        {
            var result = await _journalService.GetAllJournalsAsync();
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetJournalById(int id)
        {
            var result = await _journalService.GetJournalByIdAsync(id);
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateJournal(int id, JournalDTO journalDto)
        {
            var result = await _journalService.UpdateJournalAsync(id, journalDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteJournal(int id)
        {
            var result = await _journalService.DeleteJournalAsync(id);

            if (!result)
                return NotFound("Journal not found");

            return Ok("Journal deleted successfully");
        }
    }
}