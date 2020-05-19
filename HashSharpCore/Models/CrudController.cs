using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using HashSharpCore.DataLayer.Models;
using HashSharpCore.Filters;
using HashSharpCore.Services.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HashSharpCore.Models
{
    [ApiController]
    //[AllowAnonymous]
    [ApiResultFilter]
    [Route("api/v{version:apiVersion}/[controller]")]// api/v1/[controller]
    public class BaseController : ControllerBase
    {
        //public UserRepository UserRepository { get; set; } => property injection
        public bool UserIsAutheticated => HttpContext.User.Identity.IsAuthenticated;
    }

    [ApiVersion("1")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CrudController<TDto, TEntity> : BaseController
    where TDto : BaseDto<TDto, TEntity>, new()
    where TEntity : ApiEntity, new()
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CrudController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET:Get All Entity
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(_repositoryWrapper.SetRepository<TEntity>().TableNoTracking.ProjectTo<TDto>());
        }

        // GET:Get An Entity By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            var ent = await _repositoryWrapper.SetRepository<TEntity>().GetByIdAsync(cancellationToken, id);
            var dto = Mapper.Map<TDto>(ent);
            return Ok(dto);
        }

        // POST:Add New Entity
        [HttpPost]
        public async Task<IActionResult> PostOrginal([FromBody] TEntity ent, CancellationToken cancellationToken)
        {
            await _repositoryWrapper.SetRepository<TEntity>().AddAsync(ent, cancellationToken);
            return Ok();
        }

        // POST:Add New Entity By Dto
        [HttpPost("Dto")]
        public async Task<IActionResult> PostDto([FromBody] TDto dto, CancellationToken cancellationToken)
        {
            await _repositoryWrapper.SetRepository<TEntity>().AddAsync(dto.ToEntity(), cancellationToken);
            return Ok();
        }

        // PUT:Update Entity
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TEntity ent, CancellationToken cancellationToken)
        {
            await _repositoryWrapper.SetRepository<TEntity>().UpdateAsync(ent, cancellationToken);
            return Ok();
        }

        // DELETE:Delete Entity
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var ent = await _repositoryWrapper.SetRepository<TEntity>().GetByIdAsync(cancellationToken, id);
            await _repositoryWrapper.SetRepository<TEntity>().DeleteAsync(ent, cancellationToken);
            return Ok();
        }
    }

    [ApiVersion("1")]
    [Authorize(AuthenticationSchemes = "Bearer")]
    public class CrudController<TEntity> : BaseController
    where TEntity : ApiEntity, new()
    {
        private readonly IRepositoryWrapper _repositoryWrapper;

        public CrudController(IRepositoryWrapper repositoryWrapper)
        {
            _repositoryWrapper = repositoryWrapper;
        }

        // GET:Get All Entity
        [HttpGet]
        public async Task<IActionResult> GetAllAsync()
        {
            return Ok(_repositoryWrapper.SetRepository<TEntity>().TableNoTracking);
        }

        // GET:Get An Entity By Id
        [HttpGet("{id}")]
        public async Task<IActionResult> GetAsync(int id, CancellationToken cancellationToken)
        {
            var ent = await _repositoryWrapper.SetRepository<TEntity>().GetByIdAsync(cancellationToken, id);
            return Ok(ent);
        }

        // POST:Add New Entity
        [HttpPost]
        public async Task<IActionResult> PostDto([FromBody] TEntity ent, CancellationToken cancellationToken)
        {
            await _repositoryWrapper.SetRepository<TEntity>().AddAsync(ent, cancellationToken);
            return Ok();
        }

        // PUT:Update Entity
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] TEntity ent, CancellationToken cancellationToken)
        {
            await _repositoryWrapper.SetRepository<TEntity>().UpdateAsync(ent, cancellationToken);
            return Ok();
        }

        // DELETE:Delete Entity
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id, CancellationToken cancellationToken)
        {
            var ent = await _repositoryWrapper.SetRepository<TEntity>().GetByIdAsync(cancellationToken, id);
            await _repositoryWrapper.SetRepository<TEntity>().DeleteAsync(ent, cancellationToken);
            return Ok();
        }
    }
}
