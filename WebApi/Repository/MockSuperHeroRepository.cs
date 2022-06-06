using WebApi.Interfaces;
using WebApi.Data;

namespace WebApi.Repository
{
    public class MockSuperHeroRepository : ISuperHeroRepository
    {
        private int IdList = 3;
        public static List<SuperHero> SuperHeroListe = new List<SuperHero>
        {
                new SuperHero 
                {
                    Id = 0, 
                    HeroName= "Iron man", 
                    FirstName= "Tony", 
                    LastName= "Stark", 
                    Universe= "Marvel"
                },

                new SuperHero 
                {
                    Id = 1, 
                    HeroName= "Superman", 
                    FirstName= "Clark", 
                    LastName= "Kent", 
                    Universe= "DC"
                },

                new SuperHero 
                {
                    Id = 2, 
                    HeroName= "Batman", 
                    FirstName= "Bruce", 
                    LastName= "Wayne", 
                    Universe= "DC"
                }
        };
        
        /****** Generic Interface Methods *****/
        /*************************************/

        public List<SuperHero> Read()
        {
            return SuperHeroListe;
        }

        public SuperHero Update(SuperHero sp)
        {
            foreach (var item in SuperHeroListe)
            {
                if(item.Id == sp.Id)
                {
                    if(sp.FirstName.Equals("") != true)
                        item.FirstName = sp.FirstName;
                    
                    if(sp.HeroName.Equals("") != true)
                        item.HeroName = sp.HeroName;

                    if(sp.LastName.Equals("") != true)
                        item.LastName = sp.LastName;

                    if(sp.Universe.Equals("") != true)
                        item.Universe = sp.Universe;

                    return item;
                }
            }

            return null;
        }

        public SuperHero Delete(int id)
        {
            SuperHero s1 = new SuperHero();
            foreach (var item in SuperHeroListe)
            {
                if(id == item.Id)
                {
                    SuperHeroListe.Remove(item);
                    return item;
                }
            }

            return null;
        }

        public SuperHero ReadOne(int id)
        {
            foreach (var item in SuperHeroListe)
            {
                if(id == item.Id)
                {
                    return item;
                }
            }
            return null;
        }

        public SuperHero Create(SuperHero sp)
        {
            foreach (var item in SuperHeroListe)
            {
                if(sp.HeroName.ToLower() == item.HeroName.ToLower())
                {
                    return null;
                }
            }

            sp.Id = this.IdList;
            SuperHeroListe.Add(sp);
            return sp;
        }

        
        /****** Sp Interface Methods *****/
        /*********************************/

        public List<SuperHero> GetAllInformation(string info)
        {
            info = info.ToLower();
            List<SuperHero> listOfHeroFind = new List<SuperHero>();

            foreach (var item in SuperHeroListe)
            {
                if(item.FirstName.ToLower() == info || item.LastName.ToLower() == info || item.Universe.ToLower() == info || item.HeroName.ToLower() == info)
                {
                    listOfHeroFind.Add(item);
                }
            }

            if(listOfHeroFind.Count == 0)
            {
                return null;
            }

            return listOfHeroFind;
        }


    }
}