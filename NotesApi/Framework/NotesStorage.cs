using System.Collections.Concurrent;
using System.Collections.Generic;
using NotesApi.Domain;

namespace NotesApi.Framework
{
    public interface INotesStorage
    {
        IReadOnlyCollection<Note> GetAll();
        void Add(Note note);
        Note Find(int noteId);
    }

    public class NotesStorage : INotesStorage
    {
        private readonly ConcurrentDictionary<int, Note> _noteStorage = new ConcurrentDictionary<int, Note>();

        public IReadOnlyCollection<Note> GetAll()
        {
            return (IReadOnlyCollection<Note>) _noteStorage.Values;
        }

        public void Add(Note note)
        {
            _noteStorage.TryAdd(note.Id, note);
        }

        public Note Find(int noteId)
        {
            _noteStorage.TryGetValue(noteId, out Note value);
            return value;
        }
    }
}