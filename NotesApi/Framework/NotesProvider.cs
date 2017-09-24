using System;
using System.Collections.Generic;
using System.Linq;
using NotesApi.Application;
using NotesApi.Application.ReadModel;
using NotesApi.Domain;

namespace NotesApi.Framework
{
    public class NotesProvider : INotesProvider
    {
        private readonly INotesRepository _notesRepository;
        private readonly INotesStorage _notesStorage;

        public NotesProvider(INotesRepository notesRepository, INotesStorage notesStorage)
        {
            _notesRepository = notesRepository;
            _notesStorage = notesStorage;
        }

        public IReadOnlyCollection<ActiveNoteMeta> GetActiveNotesMeta()
        {
            var now = DateTime.UtcNow;

            return _notesStorage.GetAll()
                .Where(n => n.ExpirationDate > now || !n.ExpirationDate.HasValue)
                .Select(n => new ActiveNoteMeta {CreationDate = n.CreationDate, Id = n.Id, Title = n.Title})
                .ToArray();
        }

        public GetResourceResult<Note> GetActiveNoteById(int noteId)
        {
            var note = _notesRepository.Find(noteId);

            return note.ExpirationDate > DateTime.UtcNow || !note.ExpirationDate.HasValue
                ? GetResourceResult<Note>.Success(note)
                : GetResourceResult<Note>.ResourceNotFound;
        }
    }
}