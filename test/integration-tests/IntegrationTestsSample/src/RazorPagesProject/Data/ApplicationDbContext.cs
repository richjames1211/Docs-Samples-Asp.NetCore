using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace RazorPagesProject.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Message> Messages { get; set; }

    // <snippet1>
    public async virtual Task<List<Message>> GetMessagesAsync()
    {
        return await Messages
            .OrderBy(message => message.Text)
            .AsNoTracking()
            .ToListAsync();
    }
    // </snippet1>

    // <snippet2>
    public async virtual Task AddMessageAsync(Message message)
    {
        await Messages.AddAsync(message);
        await SaveChangesAsync();
    }
    // </snippet2>

    // <snippet3>
    public async virtual Task DeleteAllMessagesAsync()
    {
        foreach (Message message in Messages)
        {
            Messages.Remove(message);
        }

        await SaveChangesAsync();
    }
    // </snippet3>

    // <snippet4>
    public async virtual Task DeleteMessageAsync(int id)
    {
        var message = await Messages.FindAsync(id);

        if (message != null)
        {
            Messages.Remove(message);
            await SaveChangesAsync();
        }
    }
    // </snippet4>

    public void Initialize()
    {
        Messages.AddRange(GetSeedingMessages());
        SaveChanges();
    }

    public static List<Message> GetSeedingMessages()
    {
        return new List<Message>()
        {
            new Message(){ Text = "You're standing on my scarf." },
            new Message(){ Text = "Would you like a jelly baby?" },
            new Message(){ Text = "To the rational mind, nothing is inexplicable; only unexplained." }
        };
    }
}
