namespace NotesApi.Application
{
    public class NoteForm
    {
        public string Title { get; set; }
        public string Text { get; set; }
        public int MinutesToExpire { get; set; }
    }
}