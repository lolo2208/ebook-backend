using AutoMapper;
using EbookBackend.Application.Dto;
using EbookBackend.Application.Interfaces;
using EbookBackend.Domain.Entities;
using EbookBackend.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace EbookBackend.Application.Services
{
    public class AuthorService : IAuthorService
    {
        IUnitOfWork _unitOfWork;
        IMapper _mapper;

        public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AuthorDto> GetAuthorById(int idAuhtor)
        {
            try
            {
                if(idAuhtor == 0)
                {
                    throw new Exception(message: "No valid id");
                }
                else
                {
                    var author = await _unitOfWork.Authors.GetByIdAsync(idAuhtor);
                    if(author == null)
                    {
                        throw new Exception(message: "Author not founded");
                    }else
                    {
                        var authorObj = _mapper.Map<AuthorDto>(author);
                        return authorObj;
                    }
                }
            }catch(Exception)
            {
                throw;
            }
        }

        public Task<IEnumerable<AuthorDto>> GetAuthorByQuery(string query)
        {
            throw new NotImplementedException();
        }

        public async Task<AuthorDto> SaveAuthor(AuthorDto authorDto)
        {
            try
            {
                await _unitOfWork.BeginTransactionAsync();

                var auditLog = new AuditLog
                {
                    TableName = "Authors",
                };


                //Obtener author actual
                var authorObj = _mapper.Map<Author>(authorDto);

                if (authorObj.IdAuthor == 0)
                {
                    auditLog.Action = "REGISTER";
                    auditLog.Description = "Author register";
                }
                else
                {
                    var currentAuthor = await _unitOfWork.Authors.GetByIdAsync(authorObj.IdAuthor);
                    auditLog.Action = "UPDATE";
                    auditLog.Description = "Author update";
                    auditLog.OldValues = JsonSerializer.Serialize(currentAuthor);
                }

                Author author = await _unitOfWork.Authors.AddAsync(authorObj);
                auditLog.NewValues = JsonSerializer.Serialize(author);
                auditLog.CreatedAt = DateTime.Now;

                await _unitOfWork.AuditLogs.AddAsync(auditLog);


                await _unitOfWork.SaveChangesAsync();
                await _unitOfWork.CommitAsync();
                return _mapper.Map<AuthorDto>(author);
            }
            catch(Exception ex)
            {
                await _unitOfWork.RollbackAsync();
                throw new Exception(ex.Message);
            }
        }
    }
}
