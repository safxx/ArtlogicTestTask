using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using NotesApi.Application;
using NotesApi.Application.ReadModel;

namespace NotesApi.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : Controller
    {
        private readonly INotesProvider _notesProvider;
        private readonly INotesProcessor _notesProcessor;

        public NotesController(INotesProvider notesProvider, INotesProcessor notesProcessor)
        {
            _notesProvider = notesProvider;
            _notesProcessor = notesProcessor;
        }

        [HttpGet("active/meta")]
        public IEnumerable<ActiveNoteMeta> Get()
        {
            return _notesProvider.GetActiveNotesMeta();
        }

        [HttpGet("active/{noteId}")]
        public IActionResult GetActive(int noteId)
        {
            var result = _notesProvider.GetActiveNoteById(noteId);

            if (result.IsSuccess) return Ok(result.Value);

            return NotFound();
        }

        [HttpPost]
        public void Post([FromBody] NoteForm note)
        {
            _notesProcessor.Create(note);
        }
    }
}
