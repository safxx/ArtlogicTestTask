using System.Collections.Generic;
using NotesApi.Domain;

namespace NotesApi.Application.ReadModel
{
    public interface INotesProvider
    {
        IReadOnlyCollection<ActiveNoteMeta> GetActiveNotesMeta();
        GetResourceResult<Note> GetActiveNoteById(int noteId);
    }
}