using System;
using System.Collections.Generic;
using System.Linq;


namespace WebApi.Core
{
    public class ProjectRepository : IProjectRepository
    {

        // We are using the list and _fakeDatabaseID to represent what would

        // most likely be a database of some sort, with an auto-incrementing ID field:

        private List<Project> _people = new List<Project>();

        private int _fakeDatabaseID = 1;





        public ProjectRepository()
        {

            // For the moment, we will load some sample data during initialization. 

            this.Add(new Project { projectNumber = "001", areaId = 1, gardenId = 1, builderTyp = "", content = "test", declarer = "LY1", id = 1, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "002", areaId = 1, gardenId = 1, builderTyp = "", content = "test1", declarer = "LY1", id = 2, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "003", areaId = 1, gardenId = 1, builderTyp = "", content = "test2", declarer = "LY1", id = 3, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "004", areaId = 1, gardenId = 1, builderTyp = "", content = "test3", declarer = "LY2", id = 4, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "005", areaId = 1, gardenId = 1, builderTyp = "", content = "test4", declarer = "LY2", id = 5, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "006", areaId = 1, gardenId = 1, builderTyp = "", content = "test5", declarer = "LY2", id = 6, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "007", areaId = 1, gardenId = 1, builderTyp = "", content = "test1", declarer = "LY3", id = 7, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "008", areaId = 1, gardenId = 1, builderTyp = "", content = "test1", declarer = "LY3", id = 8, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "009", areaId = 1, gardenId = 1, builderTyp = "", content = "test2", declarer = "LY3", id = 9, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "010", areaId = 1, gardenId = 1, builderTyp = "", content = "test2", declarer = "LY1", id = 10, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "011", areaId = 1, gardenId = 1, builderTyp = "", content = "test3", declarer = "LY5", id = 11, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "012", areaId = 1, gardenId = 1, builderTyp = "", content = "test3", declarer = "LY6", id = 12, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "013", areaId = 1, gardenId = 1, builderTyp = "", content = "test3", declarer = "LY7", id = 13, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "014", areaId = 1, gardenId = 1, builderTyp = "", content = "test4", declarer = "LY8", id = 14, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "015", areaId = 1, gardenId = 1, builderTyp = "", content = "test4", declarer = "LY1", id = 15, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "016", areaId = 1, gardenId = 1, builderTyp = "", content = "test5", declarer = "LY2", id = 16, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "017", areaId = 1, gardenId = 1, builderTyp = "", content = "test6", declarer = "LY3", id = 17, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });
            this.Add(new Project { projectNumber = "018", areaId = 1, gardenId = 1, builderTyp = "", content = "test7", declarer = "LY4", id = 18, isRenewal = "", projectDate = DateTime.Now, simcardOwner = "", waterCorpId = 2, waterSupplyType = "" });


        }





        public IEnumerable<Project> GetAll()
        {

            return _people;

        }





        public Project Get(int id)
        {

            return _people.Find(p => p.id == id);

        }





        public Project Add(Project Project)
        {

            if (Project == null)
            {

                throw new ArgumentNullException("Project");

            }

            Project.id = _fakeDatabaseID++;

            _people.Add(Project);

            return Project;

        }





        public void Remove(int id)
        {

            _people.RemoveAll(p => p.id == id);

        }





        public bool Update(Project Project)
        {

            if (Project == null)
            {

                throw new ArgumentNullException("Project");

            }

            int index = _people.FindIndex(p => p.id == Project.id);

            if (index == -1)
            {

                return false;

            }

            _people.RemoveAt(index);

            _people.Add(Project);

            return true;

        }

    }

}