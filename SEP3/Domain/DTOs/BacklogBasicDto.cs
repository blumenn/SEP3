using Domain.Models;

namespace Domain.DTOs;

public class BacklogBasicDto
{
    public string? name { get; set; }
   
    public User? ProductOwner { get;  set; }
    public bool IsCompleted { get; set; }

    public BacklogBasicDto(string? name, User? productOwner, bool isCompleted)
    {
        this.name = name;
        ProductOwner = productOwner;
        IsCompleted = isCompleted;
    }
}