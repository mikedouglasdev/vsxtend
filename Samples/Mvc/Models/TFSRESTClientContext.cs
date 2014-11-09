using System.Data.Entity;
using Vsxtend.Samples.Mvc.Entities;

namespace Vsxtend.Samples.Mvc.Models
{
    public class TFSRESTClientContext : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, add the following
        // code to the Application_Start method in your Global.asax file.
        // Note: this will destroy and re-create your database with every model change.
        // 
        // System.Data.Entity.Database.SetInitializer(new System.Data.Entity.DropCreateDatabaseIfModelChanges<Vsxtend.Models.TFSRESTClientContext>());

        public TFSRESTClientContext() : base("name=TFSRESTClient")
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
