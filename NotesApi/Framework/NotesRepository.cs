using System.Threading;
using NotesApi.Domain;

namespace NotesApi.Framework
{
    public class NotesRepository : INotesRepository
    {
        private int _idCounter;
        private readonly INotesStorage _notesStorage;

        public NotesRepository(INotesStorage notesStorage)
        {
            _notesStorage = notesStorage;
        }

        public void Create(Note note)
        {
            note.Id = Interlocked.Increment(ref _idCounter);
            _notesStorage.Add(note);
        }

        public Note Find(int noteId)
        {
            return _notesStorage.Find(noteId);
        }
    }
}