using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

using Crud_App_dotNetCore.Entities;

namespace Crud_App_dotNetApplication.Mapping
{
    public class MappinProfiles : Profile
    {
        public MappinProfiles()
        {
            CreateMap<Journal, JournalDTO>().ReverseMap();

            CreateMap<JournalDetail, JournalDetailDTO>().ReverseMap();
        }
    }
}
