using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CRUD.API.Entities;
using CRUD.API.Persistence;
using Microsoft.EntityFrameworkCore;

namespace CRUD.API.Controllers

{
    [Route("api/dev-events")]
    [ApiController]
    public class DevEventsController : ControllerBase
    {

        private readonly DevEventsDbContent _content;

        public DevEventsController(DevEventsDbContent content)
        {
            _content = content;
        }

        /// <summary>
        /// Busca todos os eventos desde que não estejam deletados
        /// </summary>
        /// <returns>Lista de eventos</returns>
        /// <response code="200">Sucesso</response>
        [HttpGet]
        public IActionResult GetAll()
        {
            var devEvents = _content.DevEvents.Where(d => !d.IsDeleted).ToList();

            return Ok(devEvents);
        }

        /// <summary>
        /// busca o evento mesmo estando deletado
        /// </summary>
        /// <param name="id">Identificador do evento</param>
        /// <returns>Dados do evento</returns>
        /// <response code="404">Não encontrado</response>
        /// <response code="200">Sucesso</response>
        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var devEvent = _content.DevEvents
                .Include(de => de.Speakers)
                .SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }
            return Ok(devEvent);
        }

        /// <summary>
        /// Cadastrar um evento
        /// </summary>
        /// <remarks>
        /// {"title":"string","description":"string","startDate":"2023-02-27T17:59:14.141Z","endDate":"2023-02-27T17:59:14.141Z"}
        /// </remarks>
        /// <param name="devEvent">Dados do evento</param>
        /// <returns>Evento recém criado</returns>
        /// <response code="201">Sucesso</response>
        [HttpPost]
        public IActionResult Post(DevEvent devEvent)
        {
            _content.DevEvents.Add(devEvent);
            _content.SaveChanges();
            return CreatedAtAction(nameof(GetById), new { id = devEvent.Id }, devEvent);
        }

        /// <summary>
        /// Atualiza um evento
        /// </summary>
        /// <remarks>
        /// {"title":"string","description":"string","startDate":"2023-02-27T17:59:14.141Z","endDate":"2023-02-27T17:59:14.141Z"}
        /// </remarks>
        /// <param name="id">Identificador do evento</param>
        /// <param name="input">Dados a serem atualizados</param>
        /// <returns>Nada</returns>
        /// <response code="404">Não encontrado</response>
        /// <response code="204">Sucesso</response>
        [HttpPut("{id}")]
        public IActionResult Put(Guid id, DevEvent input)
        {
            var devEvent = _content.DevEvents.SingleOrDefault(d => d.Id == id);

            if (devEvent == null)
            {
                return NotFound();
            }

            devEvent.Update(input.Title, input.Description, input.StartDate, input.EndDate);

            _content.DevEvents.Update(devEvent);
            _content.SaveChanges();
            return NoContent();
        }

        /// <summary>
        /// Deleta um evento
        /// </summary>
        /// <param name="Id">Identificador do evento</param>
        /// <returns>Nada</returns>
        /// <response code="404">Não encontrado</response>
        /// <response code="204">Sucesso</response>
        [HttpDelete]
        public IActionResult Delete(Guid Id)
        {
            var devEvent = _content.DevEvents.SingleOrDefault(d => d.Id == Id);

            if (devEvent == null || devEvent.Id == Guid.Empty)
            {
                return NotFound();
            }

            devEvent.Delete();

            _content.SaveChanges();

            return NoContent();
         }

        /// <summary>
        /// Cadastrar palestrante
        /// </summary>
        /// <remarks>
        /// {"name":"string","talkTitle":"string","talkDescription":"string","linkedInProfile":"string"}
        /// </remarks>
        /// <param name="id">Identificador do evento</param>
        /// <param name="input">Dados do palestrante</param>
        /// <returns>Nada</returns>
        /// <response code="204">Sucesso</response>
        /// <response code="404">Evento não encontrado</response>
        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeakers(Guid id, DevEventSpeaker speaker)
        {
            speaker.DevEventId = id;

            var devEvent = _content.DevEvents.Any(d => d.Id == id);

            if (!devEvent)
            {
                return NotFound();
            }

            _content.DevEventsSpeaker.Add(speaker);
            _content.SaveChanges();

            return NoContent();
        }
    }
}

