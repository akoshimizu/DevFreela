namespace DevFreela.Core.Entities
{
    public class ProjectsGetAll
    {
        public ProjectsGetAll(string search, int page, int size)
        {
            this.search = search;
            this.page = page;
            this.size = size;
        }

        public string search { get; set; }
        public int page { get; set; }
        public int size { get; set; }
    }
}
