using Domain.Models;

namespace Domain.DTOs;

public class BacklogCreationDto
{
    public string backlogName { get; }
    public User user { get; }


    public BacklogCreationDto(string backlogName, User user)
    {
        this.backlogName = backlogName;
        this.user = user;
    }
}