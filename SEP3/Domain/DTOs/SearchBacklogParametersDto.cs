namespace Domain.DTOs;

public class SearchBacklogParametersDto
{
    
        public string? Name { get;}
        
        public bool? CompletedStatus { get;}
        public string? TitleContains { get;}

        public SearchBacklogParametersDto(string? Name,  bool? completedStatus, string? titleContains)
        {
            this.Name = Name;
            CompletedStatus = completedStatus;
            TitleContains = titleContains;
        }
    }
