using System;
using NotesApi.Controllers;
using NotesApi.Domain;

namespace NotesApi.Application
{
    public interface INotesProcessor
    {
        void Create(NoteForm noteForm);
    }

    public class NotesProcessor : INotesProcessor
    {
        private readonly INotesRepository _notesRepository;

        public NotesProcessor(INotesRepository notesRepository)
        {
            _notesRepository = notesRepository;
        }

        public void Create(NoteForm noteForm)
        {
            var now = DateTime.UtcNow;

            var note = new Note
            {
                CreationDate = now,
                ExpirationDate = noteForm.MinutesToExpire == 0 ? (DateTime?) null : now.AddMinutes(noteForm.MinutesToExpire),
                Text = noteForm.Text,
                Title = noteForm.Title
            };

            _notesRepository.Create(note);
        }
    }
}