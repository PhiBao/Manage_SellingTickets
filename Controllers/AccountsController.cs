using System.Collections.Generic;
using AutoMapper;
using backend.Dtos;
using backend.Models;
using backend.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;

        public AccountsController(IAccountService accountService, IMapper mapper)
        {
            _accountService = accountService;
            _mapper = mapper;
        }

        // GET api/accounts/id
        [HttpGet("{id}", Name = "GetAccountById")]
        public ActionResult<Taikhoan> GetAccountById(int id)
        {
            var account = _accountService.GetAccountById(id);

            if (account != null)
            {
                return Ok(account);
            }

            return NotFound();
        }

        // POST api/accounts
        [HttpPost]
        public ActionResult<AccountReadDto> CreateAccount(Taikhoan account)
        {
            _accountService.CreateAccount(account);

            AccountReadDto accountDto = _mapper.Map<AccountReadDto>(account);

            return CreatedAtRoute(nameof(GetAccountById), new { id = accountDto.MaNd }, accountDto);
        }

        // Put api/accounts/{id}
        [HttpPut("{id}")]
        public ActionResult UpdateAccount(int id, AccountUpdateDto accountUpdateDto) 
        {
            var accountSelected = _accountService.GetAccountById(id);
            if (accountSelected == null)
            {
                return NotFound();
            }

            _mapper.Map(accountUpdateDto, accountSelected);
            _accountService.UpdateAccount(accountSelected);
            _accountService.SaveChanges();

            return NoContent();
        }

        // PATCH api/accounts/{id} -- not necessary
        [HttpPatch("{id}")]
        public ActionResult PartialAccountUpdate(int id, JsonPatchDocument<AccountUpdateDto> patchDoc) 
        {
            var accountSelected = _accountService.GetAccountById(id);
            if (accountSelected == null)
            {
                return NotFound();
            }

            var accoutToPatch = _mapper.Map<AccountUpdateDto>(accountSelected);
            patchDoc.ApplyTo(accoutToPatch, ModelState);

            if (!TryValidateModel(accoutToPatch))
            {
                return ValidationProblem(ModelState);
            }

            _accountService.UpdateAccount(accountSelected);
            _accountService.SaveChanges();

            return NoContent();
        }

        // DELETE api/accounts/{id}
        [HttpDelete("{id}")]
        public ActionResult DeleteAccount(int id) 
        {
            var accountSelected = _accountService.GetAccountById(id);
            if (accountSelected == null)
            {
                return NotFound();
            }

            _accountService.DeleteAccount(accountSelected);

            return NoContent();
        }

    }
}