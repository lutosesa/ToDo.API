namespace ToDo.API.Models
{
    public class ToDoList
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public bool IsDone { get; set; }
    }
}