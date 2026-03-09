using AutoMapper;
using Crud_App_dotNetApplication.Interfaces;
using Crud_App_dotNetApplication.Interfaces.IServices;

namespace Crud_App_dotNetApplication.Services
{
    public class JournalService : IJournalService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public JournalService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<JournalDTO> AddJournalAsync(JournalDTO journalDto)
        {
            if (journalDto.JournalDetails == null || !journalDto.JournalDetails.Any())
                throw new Exception("Journal must contain at least one detail line.");

            var totalDebit = journalDto.JournalDetails.Sum(x => x.Debit);
            var totalCredit = journalDto.JournalDetails.Sum(x => x.Credit);

            if (totalDebit != totalCredit)
                throw new Exception("Debit and Credit must be equal.");

            var journal = _mapper.Map<Journal>(journalDto);

            journal.CreatedAt = DateTime.Now;
            journal.UpdatedAt = DateTime.Now;

            await _unitOfWork.Journal.AddAsync(journal);
            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<JournalDTO>(journal);
        }

        public async Task<IEnumerable<JournalDTO>> GetAllJournalsAsync()
        {
            var journals = await _unitOfWork.Journal.GetAllAsync();
            return _mapper.Map<IEnumerable<JournalDTO>>(journals);
        }

        public async Task<JournalDTO> GetJournalByIdAsync(int id)
        {
            var journal = await _unitOfWork.Journal.GetByIdAsync(id);

            if (journal == null)
                throw new Exception("Journal Not Found!!!");

            return _mapper.Map<JournalDTO>(journal);
        }

        public async Task<JournalDTO> UpdateJournalAsync(int id, JournalDTO journalDto)
        {
            var journal = await _unitOfWork.Journal.GetByIdAsync(id);

            if (journal == null)
                throw new Exception("Journal Not Found!!!");

            var totalDebit = journalDto.JournalDetails.Sum(x => x.Debit);
            var totalCredit = journalDto.JournalDetails.Sum(x => x.Credit);

            if (totalDebit != totalCredit)
                throw new Exception("Debit and Credit must be equal.");

            journal.VoucherDate = journalDto.VoucherDate;
            journal.VoucherType = journalDto.VoucherType;
            journal.VoucherRef = journalDto.VoucherRef;
            journal.VoucherNo = journalDto.VoucherNo;
            journal.Narration = journalDto.Narration;
            journal.StoreCode = journalDto.StoreCode;
            //journal.UserId = journalDto.UserId;
            journal.UpdatedAt = DateTime.Now;

            _unitOfWork.Journal.Update(journal);

            await _unitOfWork.SaveChangesAsync();

            return _mapper.Map<JournalDTO>(journal);
        }

        public async Task<bool> DeleteJournalAsync(int id)
        {
            var journal = await _unitOfWork.Journal.GetByIdAsync(id);

            if (journal == null)
                return false;

            _unitOfWork.Journal.Remove(journal);

            try
            {
                await _unitOfWork.SaveChangesAsync();
                return true;
            }
            catch (Microsoft.EntityFrameworkCore.DbUpdateException ex)
            {
                if (ex.InnerException != null && ex.InnerException.Message.Contains("REFERENCE constraint"))
                {
                    return false;
                }
                throw;
            }
        }
    }
}