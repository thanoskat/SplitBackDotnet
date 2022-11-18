using SplitBackDotnet.Models;

namespace SplitBackDotnet.Dtos{
    public class CreateGroupDto{

        public string Title { get; set; } = null!;
        public ICollection<Label>? GroupLabels { get; set; }

    }
}