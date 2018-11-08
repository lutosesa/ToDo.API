namespace ToDo.API.Models
{
    using System.Data.Entity;
    using System.Data.Entity.ModelConfiguration.Conventions;

    public class ToDoListContext : DbContext
    {
        public ToDoListContext() : base("DefaultConnection")
        {
        }

        public DbSet<ToDo.API.Models.ToDoList> ToDoLists { get; set; }

        #region Methods
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();

            base.OnModelCreating(modelBuilder);
        }
        #endregion

        
    }
}