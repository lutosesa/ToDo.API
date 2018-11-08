namespace ToDo.API.Migrations.App
{
    using System.Data.Entity.Migrations;
    using ToDo.API.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<ToDo.API.Models.ToDoListContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
            MigrationsDirectory = @"Migrations\App";
        }

        protected override void Seed(ToDoListContext context)
        {
            context.ToDoLists.AddOrUpdate(x => x.Id,
                        new ToDoList() { Id = 1, Description = "Something to do 1", IsDone = false },
                        new ToDoList() { Id = 2, Description = "Something to do 1", IsDone = false },
                        new ToDoList() { Id = 3, Description = "Something to do 1", IsDone = true },
                        new ToDoList() { Id = 4, Description = "Something to do 1", IsDone = false },
                        new ToDoList() { Id = 5, Description = "Something to do 1", IsDone = true },
                        new ToDoList() { Id = 7, Description = "Something to do 1", IsDone = false },
                        new ToDoList() { Id = 8, Description = "Something to do 1", IsDone = true },
                        new ToDoList() { Id = 9, Description = "Something to do 1", IsDone = false },
                        new ToDoList() { Id = 10, Description = "Something to do 1", IsDone = false });
        }
    }
}
