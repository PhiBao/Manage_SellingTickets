using System.Collections.Generic;
using System.Threading.Tasks;
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
        private readonly IUserService _userService;

        public AccountsController(IAccountService accountService, IUserService userService, IMapper mapper)
        {
            _userService = userService;
            _accountService = accountService;
            _mapper = mapper;
        }

        // GET api/accounts/{id}
        [HttpGet("{id}", Name = "GetAccountByIdAsync")]
        public async Task<ActionResult<Taikhoan>> GetAccountByIdAsync(int id)
        {
            var account = await _accountService.GetAccountByIdAsync(id);

            if (account != null)
            {
                return Ok(account);
            }

            return NotFound();
        }

        // POST api/accounts/{role}
        [HttpPost("{role}")]
        public async Task<ActionResult<AccountReadDto>> CreateAccountAsync(Taikhoan account, byte role)
        {
            await _accountService.CreateAccountAsync(account, role);

            AccountReadDto accountDto = _mapper.Map<AccountReadDto>(account);

            return CreatedAtRoute(nameof(GetAccountByIdAsync), new { id = accountDto.MaNd }, accountDto);
        }

        // PUT api/accounts/{id}
        [HttpPut("{id}")]
        public async Task<ActionResult> UpdateAccountAsync(int id, AccountUpdateDto accountUpdateDto)
        {
            var accountSelected = await _accountService.GetAccountByIdAsync(id);
            if (accountSelected == null)
            {
                return NotFound();
            }

            _mapper.Map(accountUpdateDto, accountSelected);

            await _accountService.UpdateAccountAsync(accountSelected);

            return NoContent();
        }

        /*
        // PATCH api/accounts/{id} -- not necessary
        [HttpPatch("{id}")]
        public async Task<ActionResult> PartialAccountUpdateAsync(int id, JsonPatchDocument<AccountUpdateDto> patchDoc)
        {
            var accountSelected = await _accountService.GetAccountByIdAsync(id);
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

            await _accountService.UpdateAccountAsync(accountSelected);

            return NoContent();
        }
        */

        // DELETE api/accounts/{id}
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteAccountAsync(int id)
        {
            var accountSelected = await _accountService.GetAccountByIdAsync(id);
            if (accountSelected == null)
            {
                return NotFound();
            }

            await _accountService.DeleteAccountAsync(accountSelected);

            return NoContent();
        }

    }
}