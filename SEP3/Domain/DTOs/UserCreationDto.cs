namespace Domain.DTOs;

public class UserCreationDto
{
    public string? userName { get;}

    public UserCreationDto(string? userName)
    {
        this.userName = userName;
    }
}