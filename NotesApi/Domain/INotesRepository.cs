namespace NotesApi.Domain
{
    public interface INotesRepository
    {
        Note Find(int noteId);
        void Create(Note note);
    }
}
